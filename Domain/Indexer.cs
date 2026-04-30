using Minimon.Domain.Moves;
using Minimon.Domain.Talents;

namespace Minimon.Domain;

public static class Indexer
{
    #region Simevine

    // description:
    // Physical Attack \ Life Resistence
    // Tank Fighter && Anti-Setup
    static MoveSet GetSimevineMoveSet()
        => MoveSet.New()
            .Add(new PunchMove(), 1)
            .Add(new KickMove(), 1)
            .Add(new LegBreakerMove(), 2)
            .Add(new TwistArmMove(), 4)
            .Add(new HardPunchMove(), 8)
            .Add(new HardKickMove(), 12)
            .Add(new WhipeMove(), 14)
            .Add(new LightDewMove(), 18);
    
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
            MoveSet = GetSimevineMoveSet()
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
            MoveSet = GetSimevineMoveSet()
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
            MoveSet = GetSimevineMoveSet()
        };
    
    #endregion

    #region Lemiember

    // description:
    // Weak Attack \ Physical Resistence
    // Bruiser Fighter && Pivot
    static MoveSet GetLemiemberMoveSet()
        => MoveSet.New()
            .Add(new CutMove(), 1)
            .Add(new ScrewUpMove(), 1)
            .Add(new SwapMove(), 2)
            .Add(new ScareMove(), 4)
            .Add(new StealMove(), 8)
            .Add(new QuickTouchMove(), 12)
            .Add(new FireBreathMove(), 14)
            .Add(new HotStrategistMove(), 18);
    
    static Species? _Lemiolil;
    public static Species Lemiolil => _Lemiolil ??=
        new() {
            Name = "Lemiolil",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Lemiont,

            BaseLife = 6,
            BaseTechnique = 2,
            BasePhysicalDefense = 4,
            BaseMagicalDefense = 0,
            SpeedIndex = 116,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetLemiemberMoveSet()
        };
        
    static Species? _Lemiont;
    public static Species Lemiont => _Lemiont ??=
        new() {
            Name = "Lemiont",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Lemiember,

            BaseLife = 7,
            BaseTechnique = 3,
            BasePhysicalDefense = 6,
            BaseMagicalDefense = 0,
            SpeedIndex = 116,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetLemiemberMoveSet()
        };
        
    static Species? _Lemiember;
    public static Species Lemiember => _Lemiember ??=
        new() {
            Name = "Lemiember",
            MainType = Type.Normal,
            SecondType = Type.Fire,
            Evolution = null,

            BaseLife = 8,
            BaseTechnique = 4,
            BasePhysicalDefense = 8,
            BaseMagicalDefense = 0,
            SpeedIndex = 116,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetLemiemberMoveSet()
        };

    #endregion

    #region Tarsuriver

    // description:
    // Strong Attack \ Magical Resistence
    // Assassin Fighter && Sweeper
    
    static MoveSet GetTarsuriverMoveSet()
        => MoveSet.New()
            .Add(new ScratchMove(), 1)
            .Add(new BreakMove(), 1);
            // .Add(new missing(), 2)
            // .Add(new missing(), 4)
            // .Add(new missing(), 8)
            // .Add(new missing(), 12)
            // .Add(new missing(), 14)
            // .Add(new missing(), 18);
    
    static Species? _Tarsuolil;
    public static Species Tarsuolil => _Tarsuolil ??=
        new() {
            Name = "Tarsuolil",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Tarsuont,

            BaseLife = 5,
            BaseTechnique = 4,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 3,
            SpeedIndex = 132,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetLemiemberMoveSet()
        };
        
    static Species? _Tarsuont;
    public static Species Tarsuont => _Tarsuont ??=
        new() {
            Name = "Tarsuont",
            MainType = Type.Normal,
            SecondType = null,
            Evolution = Tarsuriver,

            BaseLife = 6,
            BaseTechnique = 6,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 4,
            SpeedIndex = 132,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetLemiemberMoveSet()
        };
        
    static Species? _Tarsuriver;
    public static Species Tarsuriver => _Tarsuriver ??=
        new() {
            Name = "Tarsuriver",
            MainType = Type.Normal,
            SecondType = Type.Aquatic,
            Evolution = null,

            BaseLife = 6,
            BaseTechnique = 8,
            BasePhysicalDefense = 0,
            BaseMagicalDefense = 6,
            SpeedIndex = 132,
            
            Talent = new UnfairAdvantage(),
            MoveSet = GetTarsuriverMoveSet()
        };


    #endregion
}