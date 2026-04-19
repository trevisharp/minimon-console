namespace Minimon.Domain.Moves;

public class KickMove() : Move("Move", Type.Normal, 6, 2, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Strong, 2);
        return true;
    }
}