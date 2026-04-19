namespace Minimon.Domain.Moves;

public class FastGuardMove() : Move("Fast Guard", Type.Normal, 10, 8, 4)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        if (ctx.TurnMoveCount > 0)
            return false;
        
        ctx.Char.ClearEffect();
        ctx.Char.Apply(Effect.Protected, 0);
        return true;
    }
}