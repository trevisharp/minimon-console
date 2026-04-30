namespace Minimon.Domain.Moves;

public class KickMove() : Move("Kick", Type.Normal, -1, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Physical, Type.Normal, 6);
        return true;
    }
}