using System;

namespace Minimon.Domain;

public class Creature(Species species, int level = 1)
{
    public Species Species { get; private set; } = species;
    public int Level { get; private set; } = level;
    public int Experience { get; private set; } = NeededXP(level);
    public int UpgradePoints { get; private set; } = 0;
    public int LifeUpgrade { get; private set; } = 0;
    public int PhysicalDefenseUpgrade { get; private set; } = 0;
    public int MagicalDefenseUpgrade { get; private set; } = 0;
    public int SpeedUpgrade { get; private set; } = 0;
    public int StaminaUpgrade { get; private set; } = 0;
    public int AbilityUpgrade { get; private set; } = 0;
    
    public int CurrentLife { get; private set; }
    public int CurrentPhysicalShield { get; private set; }
    public int CurrentMagicalShield { get; private set; }
    public int CurrentStamina { get; private set; }
    public Effect CurrentEffect { get; private set; }

    public void Heal()
    {
        CurrentLife = Life;
        CurrentPhysicalShield = PhysicalDefense;
        CurrentMagicalShield = MagicalDefense;
        CurrentStamina = Stamina;
        CurrentEffect = Effect.None;

        OnHeal?.Invoke();
    }

    public XPEarnResult EarnExperience(int xp)
    {
        Experience += xp;
        if (Level == 20)
            return new(false, false, false);
        
        // S(n) = Sum of Fibonnacci = F(n + 2) - 1
        // needed = Level to get level n 
        //  = Sum of all xp on next level
        //  = 10 * S(n + 1)
        // But level 0 and level 1 may be desconsidered, so:
        // needed = 10 * (S(n + 1) - 2)
        // So:
        // needed = 10 * (F(n + 3) - 3)
        var needed = NeededXP(Level + 1);
        var winUpgrade = false;

        while (Experience >= needed && Level < 20)
        {
            Level++;

            (winUpgrade, UpgradePoints) = Level switch
            {
                6 => (true, UpgradePoints + 2),
                10 or 16 => (true, UpgradePoints + 3),
                20 => (true, UpgradePoints + 4),
                _ => (winUpgrade, UpgradePoints)
            };

            needed += 10 * Fibonnacci(Level + 1);
        }

        return new(true, true, winUpgrade);
    }

    public bool Upgrade(Status status)
    {
        if (UpgradePoints == 0)
            return false;

        var propName = $"{status}Upgrade";
        foreach (var prop in typeof(Creature).GetProperties())
        {
            if (prop.Name != propName)
                continue;
            
            var value = prop.GetValue(this) as int?;
            if (!value.HasValue)
                return false;
            
            if (value >= 6)
                return false;
            
            prop.SetValue(this, value + 1);
            UpgradePoints--;
            return true;
        }

        return false;
    }

    public DamageResult Recive(DamageType type, int value)
    {
        OnReceiveDamage?.Invoke(value);

        var (PhyBreak, MagBreak) = type switch
        {
            DamageType.Physical => HandlePhysical(value),
            DamageType.Magical  => HandleMagical(value),
            DamageType.Strong   => HandleStrong(value),
            DamageType.Weak     => HandleWeak(value),
            DamageType.Real     => HandleReal(value),
            _ => (false, false)
        };

        if (CurrentEffect == 0)
            OnFaint?.Invoke();

        if (PhyBreak)
            OnPhysicalShieldBreak?.Invoke();

        if (MagBreak)
            OnMagicalShieldBreak?.Invoke();
        
        return new(CurrentLife == 0, PhyBreak, MagBreak);

        (bool, bool) HandlePhysical(int damage)
        {
            OnReceivePhysicalDamage?.Invoke(damage);
            CurrentPhysicalShield = Inflicts(CurrentPhysicalShield, ref damage);
            CurrentLife = Inflicts(CurrentLife, ref damage);
            return (CurrentPhysicalShield == 0, false);
        }

        (bool, bool) HandleMagical(int damage)
        {
            OnReceiveMagicalDamage?.Invoke(damage);

            CurrentMagicalShield = Inflicts(CurrentMagicalShield, ref damage);
            CurrentLife = Inflicts(CurrentLife, ref damage);
            return (false, CurrentMagicalShield == 0);
        }

        (bool, bool) HandleStrong(int damage)
        {
            OnReceiveStrongDamage?.Invoke(damage);

            if (CurrentMagicalShield == 0)
            {
                CurrentLife = Inflicts(CurrentLife, ref damage);
                return (false, false);
            }

            if (CurrentPhysicalShield == 0)
            {
                CurrentLife = Inflicts(CurrentLife, ref damage);
                return (false, false);
            }

            if (CurrentPhysicalShield < CurrentMagicalShield)
            {
                CurrentPhysicalShield = Inflicts(CurrentPhysicalShield, ref damage);
                CurrentLife = Inflicts(CurrentLife, ref damage);
                return (CurrentPhysicalShield == 0, false);
            }
            
            CurrentMagicalShield = Inflicts(CurrentMagicalShield, ref damage);
            CurrentLife = Inflicts(CurrentLife, ref damage);
            return (false, CurrentMagicalShield == 0);
        }

        (bool, bool) HandleWeak(int damage)
        {
            OnReceiveWeakDamage?.Invoke(damage);

            if (CurrentMagicalShield == 0 && CurrentPhysicalShield == 0)
            {
                CurrentLife = Inflicts(CurrentLife, ref damage);
                return (false, false);
            }

            if (CurrentPhysicalShield > CurrentMagicalShield)
            {
                var diff = CurrentPhysicalShield - CurrentMagicalShield;
                var final = int.Min(damage, diff);
                damage -= final;
                CurrentPhysicalShield = Inflicts(CurrentPhysicalShield, ref final);
                if (damage == 0)
                    return (CurrentPhysicalShield == 0, false);
            }
            else if (CurrentMagicalShield > CurrentPhysicalShield)
            {
                var diff = CurrentMagicalShield - CurrentPhysicalShield;
                var final = int.Min(damage, diff);
                damage -= final;
                CurrentMagicalShield = Inflicts(CurrentMagicalShield, ref final);
                if (damage == 0)
                    return (false, CurrentMagicalShield == 0);
            }

            int magdam = damage / 2;
            int phydam = damage - magdam;

            CurrentMagicalShield = Inflicts(CurrentMagicalShield, ref magdam);
            CurrentPhysicalShield = Inflicts(CurrentPhysicalShield, ref phydam);
            
            damage = magdam + phydam;
            
            CurrentMagicalShield = Inflicts(CurrentMagicalShield, ref damage);
            CurrentPhysicalShield = Inflicts(CurrentPhysicalShield, ref damage);
            CurrentLife = Inflicts(CurrentLife, ref damage);
            
            return (CurrentPhysicalShield == 0, CurrentMagicalShield == 0);
        }

        (bool, bool) HandleReal(int damage)
        {
            OnReceiveRealDamage?.Invoke(damage);

            CurrentLife = Inflicts(CurrentLife, ref damage);
            return (false, false);
        }
    }
    
    public bool SpendStamina(int value)
    {
        if (CurrentStamina < value)
            return false;
        
        CurrentStamina -= value;
        return true;
    }

    public bool Apply(Effect effect)
    {
        if (CurrentEffect != Effect.None)
            return false;
        
        if (CurrentEffect == effect)
            return false;
        
        CurrentEffect = effect;
        OnReceiveEffect?.Invoke(effect);
        return true;
    }

    public void FindEnemy(Creature enemy)
    {
        OnEnemyFind?.Invoke(enemy);
    }

    public int GetSpeed(int moveSpeed, int advantage)
    {
        var extraSpeed = SpeedUpgrade + advantage;
        var speedBase = 4096 * (moveSpeed + extraSpeed);
        var speedIndex = 8 * Species.SpeedIndex + 33 * extraSpeed;
        return RoundByLevel(speedBase + speedIndex);
    }

    public bool Evolve()
    {
        if (!CanEvolve || Species.Evolution is null)
            return false;

        Species = Species.Evolution;
        return true;
    }
    
    public void CommitTurn()
    {
        CurrentStamina = Stamina;
        OnTurn?.Invoke();
    }

    public bool CanEvolve =>
        (Level, Species.Evolution, Species.Evolution?.Evolution) switch
        {
            (<8, _, _) => false,
            (>=8, _, not null) => true,
            (<13, _, _) => false,
            (>=13, not null, _) => true,
            _ => false
        };
    public int CurrentXP => Level == 20 ? 0 : Experience - NeededXP(Level);
    public int LevelXP => Level == 20 ? 0 : 10 * Fibonnacci(Level + 1);
    public int Life => 2 * LifeUpgrade + RoundByLevel(2 * Species.BaseLife);
    public int PhysicalDefense => 4 * PhysicalDefenseUpgrade + RoundByLevel(4 * Species.BasePhysicalDefense);
    public int MagicalDefense => 4 * MagicalDefenseUpgrade + RoundByLevel(4 * Species.BaseMagicalDefense);
    public int Stamina => 4 * StaminaUpgrade + RoundByLevel(8 * Species.BaseStamina);
    public int Ability => AbilityUpgrade + RoundByLevel(Species.BaseAbility);

    public event Action? OnFaint;
    public event Action? OnHeal;
    public event Action? OnPhysicalShieldBreak;
    public event Action? OnMagicalShieldBreak;
    public event Action<int>? OnReceiveDamage;
    public event Action<int>? OnReceivePhysicalDamage;
    public event Action<int>? OnReceiveMagicalDamage;
    public event Action<int>? OnReceiveStrongDamage;
    public event Action<int>? OnReceiveWeakDamage;
    public event Action<int>? OnReceiveRealDamage;
    public event Action<Effect>? OnReceiveEffect;
    public event Action? OnTurn;
    public event Action<Creature>? OnEnemyFind;

    int RoundByLevel(int value)
        => (int)float.Round((Level + 20) * value / 40);
    
    static int Inflicts(int defense, ref int damage)
    {
        if (damage > defense)
        {
            damage -= defense;
            return 0;
        }

        defense -= damage;
        damage = 0;
        return defense;
    }

    /// <summary>
    /// Get the total XP needed to get a specific level.
    /// </summary>
    static int NeededXP(int level)
    {
        if (level < 2)
            return 0;
        
        // S(n) = Sum of Fibonnacci = F(n + 2) - 1
        // needed = Level to get level n 
        //  = Sum of all xp on next level
        //  = 10 * S(n)
        // But level 1 may be desconsidered, so:
        // needed = 10 * (S(n) - 1)
        // So:
        // needed = 10 * (F(n + 2) - 2)
        return 10 * (Fibonnacci(level + 2) - 2);
    }

    static int Fibonnacci(int n)
    {
        if (n < 2)
            return 1;
        
        var s0 = 1;
        var s1 = 1;

        for (int k = 2; k < n; k++)
        {
            s0 += s1;
            (s0, s1) = (s1, s0);
        }

        return s1;
    }

    public static Creature FromSpecies(Species species, int level = 1)
    {
        var creature = new Creature(species, level);
        creature.Heal();
        return creature;
    }

    public record XPEarnResult(bool WinXp, bool WinLevel, bool WinUpgrade);
    public record DamageResult(bool Fainted, bool PhysicalBreak, bool MagicalBreak);
}