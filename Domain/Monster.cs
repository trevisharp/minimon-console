using System;

namespace Minimon.Domain;

public class Monster
{
    public required Species Species { get; init; }
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; } = 0;
    public int UpgradePoints { get; private set; } = 0;
    public int LifeUpgrade { get; private set; } = 0;
    public int PhysicalDefenseUpgrade { get; private set; } = 0;
    public int MagicalDefenseUpgrade { get; private set; } = 0;
    public int SpeedIndexUpgrade { get; private set; } = 0;
    public int StaminaUpgrade { get; private set; } = 0;
    public int AbilityUpgrade { get; private set; } = 0;

    public XPEarnResult EarnExperience(int xp)
    {
        if (Level == 20)
            return new(false, false, false);
        Experience += xp;
        
        // S(n) = Sum of Fibonnacci = F(n + 2) - 1
        // needed = Level to get level n 
        //  = Sum of all xp on next level
        //  = 10 * S(n + 1)
        // But level 0 and level 1 may be desconsidered, so:
        // needed = 10 * (S(n + 1) - 2)
        // So:
        // needed = 10 * (F(n + 3) - 3)
        var needed = 10 * (Fibonnacci(Level + 3) - 3);
        var winUpgrade = false;

        while (Experience < needed && Level < 20)
        {
            Level++;

            (winUpgrade, UpgradePoints) = Level switch
            {
                6 => (true, UpgradePoints + 2),
                11 or 15 => (true, UpgradePoints + 3),
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
        foreach (var prop in typeof(Monster).GetProperties())
        {
            if (prop.Name != propName)
                continue;
            
            var value = prop.GetValue(this) as int?;
            if (!value.HasValue)
                return false;
            
            prop.SetValue(this, value + 1);
            UpgradePoints--;
            return true;
        }

        return false;
    }

    public int Life => 2 * LifeUpgrade + RoundByLevel(2 * Species.BaseLife);
    public int PhysicalDefense => 4 * PhysicalDefenseUpgrade + RoundByLevel(4 * Species.BasePhysicalDefense);
    public int MagicalDefense => 4 * MagicalDefenseUpgrade + RoundByLevel(4 * Species.BaseMagicalDefense);
    public int SpeedIndex => 16 * SpeedIndexUpgrade + RoundByLevel(Species.SpeedIndex);
    public int Stamina => 4 * StaminaUpgrade + RoundByLevel(8 * Species.BaseStamina);
    public int Ability => AbilityUpgrade + RoundByLevel(Species.BaseAbility);

    int RoundByLevel(int value)
        => (int)float.Round((Level + 20) * value / 40);

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

    public static Monster FromSpecies(Species species)
        => new() { Species = species };

    public record XPEarnResult(bool WinXp, bool WinLevel, bool WinUpgrade);
}