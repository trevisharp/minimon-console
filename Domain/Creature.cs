using System;
using System.Collections.Generic;
using System.Linq;

namespace Minimon.Domain;

public class Creature(Species species)
{
    public Species Species { get; private set; } = species;
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; } = 0;
    public int UpgradePoints { get; private set; } = 0;
    public int LifeUpgrade { get; private set; } = 0;
    public int PhysicalDefenseUpgrade { get; private set; } = 0;
    public int MagicalDefenseUpgrade { get; private set; } = 0;
    public int TechniqueUpgrade { get; private set; } = 0;
    public int SpeedUpgrade { get; private set; } = 0;
    
    public int CurrentLife { get; private set; }
    public int CurrentPhysicalShield { get; private set; }
    public int CurrentMagicalShield { get; private set; }
    public int CurrentEffectDuration { get; set; }
    public Effect CurrentEffect { get; private set; }
    public Move[] CurrentMoves { get; private set; } = new Move[4];

    public bool Learn(Move move, int index)
    {
        if (!MoveSet.Contains(move))
            return false;
        
        CurrentMoves[index] = move;
        return true;
    }

    public void Heal()
    {
        CurrentLife = Life;
        CurrentPhysicalShield = PhysicalDefense;
        CurrentMagicalShield = MagicalDefense;
        CurrentEffect = Effect.None;
        ResetMoves();

        OnHeal?.Invoke();
    }

    public void ResetMoves()
    {
        foreach (var move in CurrentMoves)
            move?.Reset();
    }

    public void ClearEvents()
    {
        OnFaint = null;
        OnHeal = null;
        OnPhysicalShieldBreak = null;
        OnMagicalShieldBreak = null;
        OnReceiveDamage = null;
        OnReceivePhysicalDamage = null;
        OnReceiveMagicalDamage = null;
        OnReceiveRealDamage = null;
        OnDealDamage = null;
        OnDealPhysicalDamage = null;
        OnDealMagicalDamage = null;
        OnDealRealDamage = null;
        OnReceiveEffect = null;
        OnTurn = null;
        OnEnemyFind = null;
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
            
            if (value >= 8)
                return false;
            
            prop.SetValue(this, value + 1);
            UpgradePoints--;
            return true;
        }

        return false;
    }

    public DamageResult Recive(DamageType type, int value)
    {
        if (this.CurrentEffect == Effect.Protected)
            return new DamageResult(false, false, false);
        
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
            if (MagicalDefense <= PhysicalDefense)
                return HandleMagical(damage);
            
            return HandlePhysical(damage);
        }

        (bool, bool) HandleWeak(int damage)
        {
            if (MagicalDefense > PhysicalDefense)
                return HandleMagical(damage);
            
            return HandlePhysical(damage);
        }

        (bool, bool) HandleReal(int damage)
        {
            OnReceiveRealDamage?.Invoke(damage);

            CurrentLife = Inflicts(CurrentLife, ref damage);
            return (false, false);
        }
    }
    
    public int Deal(DamageType type, int value, Random? rand = null)
    {
        rand ??= Random.Shared;
        value = DiceSimulation(value, Technique, rand);

        if (OnDealDamage != null)
            value = OnDealDamage(value);
        
        value = type switch
        {
            DamageType.Physical when 
                OnDealPhysicalDamage is not null 
                => OnDealPhysicalDamage(value),
                
            DamageType.Magical when 
                OnDealMagicalDamage is not null 
                => OnDealMagicalDamage(value),
                
            DamageType.Real when 
                OnDealRealDamage is not null 
                => OnDealRealDamage(value),
            
            _ => value
        };

        return value;
    }

    public void ClearEffect()
        => CurrentEffect = Effect.None;

    public bool Apply(Effect effect, int duration = -1)
    {
        if (CurrentEffect != Effect.None)
            return false;
        
        if (CurrentEffect == effect)
            return false;
        
        CurrentEffectDuration = duration;
        CurrentEffect = effect;
        OnReceiveEffect?.Invoke(effect);
        return true;
    }

    public void FindEnemy(Creature enemy)
    {
        OnEnemyFind?.Invoke(enemy);
    }

    public int GetSpeed(int actionSpeed)
    {
        const int maxSpeedIndex = 8 * 1024;
        var speedBase = maxSpeedIndex * (4 * actionSpeed + SpeedUpgrade);
        var speedIndex = 8 * Species.SpeedIndex + 33 * SpeedUpgrade;
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
        if (CurrentEffectDuration == 0)
            CurrentEffect = Effect.None;
        
        if (CurrentEffectDuration > 0)
            CurrentEffectDuration--;
        
        OnTurn?.Invoke();
    }

    public bool HasAdvantage(Creature other)
    {
        var advSet = Species.MainType.AdvantageSet
            .Union(Species.SecondType?.AdvantageSet ?? []);
        return advSet.Contains(other.Species.MainType.Name) ||
            advSet.Contains(other.Species.SecondType?.Name);
    }

    public bool CanEvolve =>
        (Level, Species.Evolution, Species.Evolution?.Evolution) switch
        {
            (<8, _, _) => false,
            (>=8, _, not null) => true,
            (<14, _, _) => false,
            (>=14, not null, _) => true,
            _ => false
        };
    public int CurrentXP => Level == 20 ? 0 : Experience - NeededXP(Level);
    public int LevelXP => Level == 20 ? 0 : 10 * Fibonnacci(Level + 1);
    public int Life => 2 * LifeUpgrade + RoundByLevel(2 * Species.BaseLife);
    public int PhysicalDefense => 4 * PhysicalDefenseUpgrade + RoundByLevel(4 * Species.BasePhysicalDefense);
    public int MagicalDefense => 4 * MagicalDefenseUpgrade + RoundByLevel(4 * Species.BaseMagicalDefense);
    public int Technique => 2 * TechniqueUpgrade + RoundByLevel(2 * Species.BaseTechnique);

    public Move[] MoveSet => Species.MoveSet.GetMoves(Level);
    
    public event Action? OnFaint;
    public event Action? OnHeal;
    public event Action? OnPhysicalShieldBreak;
    public event Action? OnMagicalShieldBreak;
    public event Action<int>? OnReceiveDamage;
    public event Action<int>? OnReceivePhysicalDamage;
    public event Action<int>? OnReceiveMagicalDamage;
    public event Action<int>? OnReceiveRealDamage;
    public event Func<int, int>? OnDealDamage;
    public event Func<int, int>? OnDealPhysicalDamage;
    public event Func<int, int>? OnDealMagicalDamage;
    public event Func<int, int>? OnDealRealDamage;
    public event Action<Effect>? OnReceiveEffect;
    public event Action? OnTurn;
    public event Action<Creature>? OnEnemyFind;

    int RoundByLevel(int value)
        => (int)float.Round((Level + 20) * value / 40);
    
    static int DiceSimulation(int dices, int rerolls, Random rand)
    {
        int hits = 0;
        int misses = 0;

        RollAll();

        TryBasicStrategy();
        
        if (rerolls == 0)
            return hits;
        
        TrySmartStrategy();

        return hits;

        void Dice()
        {
            var dice = rand.Next(6);
            if (dice is <3)
                misses++;
            
            if (dice is >=3)
                hits++;
            
            if (dice is <5)
                dices--;
        }

        void RollAll()
        {
            while (dices > 0)
                Dice();
        }

        void TryRerollMisses()
        {
            var rerollTries = int.Min(misses, rerolls);
            misses -= rerollTries;
            rerolls -= rerollTries;
            dices += rerollTries;
            
            RollAll();
        }

        void TryBasicStrategy()
        {   
            while (misses > 0 && rerolls > 0)
                TryRerollMisses();
        }

        void TryRerollHits()
        {
            var rerollTries = int.Min(hits, rerolls / 3);
            hits -= rerollTries;
            rerolls -= rerollTries;
            dices += rerollTries;
            
            RollAll();
            TryBasicStrategy();
        }

        void TrySmartStrategy()
        {
            while (rerolls > 3)
                TryRerollHits();
        }
    }

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

    public static Creature FromSpecies(Species species)
    {
        var creature = new Creature(species);
        creature.Heal();

        var index = 0;
        foreach (var move in creature.MoveSet)
            creature.Learn(move, index++);

        return creature;
    }

    public record XPEarnResult(bool WinXp, bool WinLevel, bool WinUpgrade);
    public record DamageResult(bool Fainted, bool PhysicalBreak, bool MagicalBreak);
}