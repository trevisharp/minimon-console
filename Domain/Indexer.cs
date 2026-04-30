using Minimon.Domain.Moves;
using Minimon.Domain.Talents;

namespace Minimon.Domain;

public static class Indexer
{
    #region Simeolil

    // description:
    // Life-Tank \ Physical Damage
    static MoveSet GetSimeolilMoveSet()
        => MoveSet.New()
            .Add(new PunchMove(), 1)
            .Add(new KickMove(), 1)
            .Add(new EnganeMove(), 3)
            .Add(new InterceptMove(), 5)
            .Add(new WhipeMove(), 7)
            .Add(new LeafCutMove(), 11)
            .Add(new WaterPunchMove(), 13)
            .Add(new FastGuardMove(), 17);
    
    static Species? _Simeolil;
    public static Species Simeolil => _Simeolil ??=
        new() {
            Name = "Simeolil",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Simeont,

            BaseLife = 12,
            BaseTechnique = 0,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            SpeedIndex = 100,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetSimeolilMoveSet()
        };
        
    static Species? _Simeont;
    public static Species Simeont => _Simeont ??=
        new() {
            Name = "Simeont",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Simevine,

            BaseLife = 16,
            BaseTechnique = 0,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            SpeedIndex = 100,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetSimeolilMoveSet()
        };
        
    static Species? _Simevine;
    public static Species Simevine => _Simevine ??=
        new() {
            Name = "Simevine",
            MainType = Type.Normal,
            SecondType = Type.Plant,
            Evolution = null,

            BaseLife = 20,
            BaseTechnique = 0,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 0,
            SpeedIndex = 100,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetSimeolilMoveSet()
        };
    
    #endregion
}