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
            Evolution = null,

            BaseLife = 12,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            BaseStamina = 4,
            BaseAbility = 4,
            SpeedIndex = 100,
            
            Talent = new OverSkill(),
            MoveSet = MoveSet.New()
        };
}