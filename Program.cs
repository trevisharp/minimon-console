using Minimon.Domain;

var test = new Species
{
    BaseAbility = 0,
    BaseLife = 0,
    BaseMagicalDefense = 0,
    BasePhysicalDefense = 0,
    BaseStamina = 0,
    Evolution = null,
    MainType = null,
    Name = null,
    SecondType = null,
    SpeedIndex = 0,
    Talent = null
};

for (int i = 1; i < 21; i++)
{
    var monster = Creature.FromSpecies(test, i);
    monster.EarnExperience(500);
    System.Console.WriteLine($"Level {monster.Level} ({monster.CurrentXP} / {monster.LevelXP})");
    System.Console.WriteLine($"XP: {monster.Experience}");
    System.Console.WriteLine();
}
