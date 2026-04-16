using Minimon.Domain.Talents;

namespace Minimon.Domain;

public static class Indexer
{
    static Species? _Simeolil;
    public static Species Simeolil => _Simeolil ??=
        new() {
            Name = "Simeolil",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Simeont,

            BaseLife = 12,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            BaseStamina = 4,
            BaseAbility = 4,
            SpeedIndex = 100,
            
            Talent = new OverSkill(),
            MoveSet = MoveSet.New()
        };
        
    static Species? _Simeont;
    public static Species Simeont => _Simeont ??=
        new() {
            Name = "Simeont",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Simevine,

            BaseLife = 14,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            BaseStamina = 5,
            BaseAbility = 5,
            SpeedIndex = 100,
            
            Talent = new OverSkill(),
            MoveSet = MoveSet.New()
        };
        
    static Species? _Simevine;
    public static Species Simevine => _Simevine ??=
        new() {
            Name = "Simevine",
            MainType = Type.Normal,
            SecondType = Type.Plant,
            Evolution = null,

            BaseLife = 18,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            BaseStamina = 5,
            BaseAbility = 5,
            SpeedIndex = 100,
            
            Talent = new OverSkill(),
            MoveSet = MoveSet.New()
        };
}