using Minimon.Domain.Moves;
using Minimon.Domain.Talents;

namespace Minimon.Domain;

public static class Indexer
{
    #region Simeolil

    // description:
    // Physical Attack \ Life Resistence
    // Tank Fighter && Anti-Setup
    static MoveSet GetSimeolilMoveSet()
        => MoveSet.New()
            .Add(new PunchMove(), 1)
            .Add(new KickMove(), 1)
            .Add(new LegBreakerMove(), 2)
            .Add(new TwistArmMove(), 4)
            .Add(new HardPunchMove(), 8)
            .Add(new HardKickMove(), 12)
            .Add(new WhipeMove(), 14)
            .Add(new LightRainMove(), 18);
    
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