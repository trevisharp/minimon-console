namespace Minimon.Domain;

public class Monster
{
    public required Species Species { get; init; }
    public int Level { get; private set; } = 1;
    public int LifeUpgrade { get; private set; } = 0;
    public int DefenseUpgrade { get; private set; } = 0;
    public int SpeedUpgrade { get; private set; } = 0;
    public int StrengthUpgrade { get; private set; } = 0;
    public int AgilityUpgrade { get; private set; } = 0;
    public int InteligenceUpgrade { get; private set; } = 0;

    public bool Upgrade(Status status)
    {
        foreach (var prop in typeof(Monster).GetProperties())
        {
            if (prop.Name != status.ToString())
                continue;
            
            var value = prop.GetValue(this) as int?;
            if (!value.HasValue || value >= 100)
                return false;
            
            prop.SetValue(this, value + 1);
            return true;
        }

        return false;
    }

    public int Life => 50 + 100 * (Species.BaseLife + LifeUpgrade) / 100;
    public int Defense => 10 + 100 * (Species.BaseDefense + DefenseUpgrade) / 100;
    public int Speed => 10 + 100 * (Species.BaseSpeed + SpeedUpgrade) / 100;
    public int Strength => 10 + 100 * (Species.BaseStrength + StrengthUpgrade) / 100;
    public int Agility => 10 + 100 * (Species.BaseAgility + AgilityUpgrade) / 100;
    public int Inteligence => 10 + 100 * (Species.BaseInteligence + InteligenceUpgrade) / 100;

    public static Monster FromSpecies(Species species)
        => new() { Species = species };
}