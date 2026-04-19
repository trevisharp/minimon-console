namespace Minimon.Domain.Moves;

public class EnganeMove() : Move("Engage", Type.Normal, 4, 8, 3)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        if (ctx.TurnMoveCount > 0)
            return false;
        
        ctx.Enemy.Recive(DamageType.Weak, 12);
        return true;
    }
}