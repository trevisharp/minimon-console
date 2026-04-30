namespace Minimon.Domain.Moves;

public class BreakMove() : Move("Break", Type.Normal, -1, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Strong, Type.Normal, 4);
        return true;
    }
}