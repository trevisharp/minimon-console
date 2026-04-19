namespace Minimon.Domain.Moves;

public class InterceptMove() : Move("Intercept", Type.Normal, 8, 8, 3)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        if (ctx.TurnMoveRemaining > 1)
            return false;
        
        ctx.Enemy.Recive(DamageType.Strong, 6);
        return true;
    }
}