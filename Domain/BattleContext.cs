namespace Minimon.Domain;

public class BattleContext(Creature fst, Creature snd)
{
    public Creature Char { get; private set; } = fst;
    public Creature Enemy { get; private set; } = snd;
    public int TurnMoveCount { get; private set; } = 0;
    public int TurnMoveRemaining { get; private set; } = 0;
}