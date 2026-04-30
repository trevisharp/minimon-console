namespace Minimon.Domain.Moves;

public class KickMove() : Move("Kick", Type.Normal, -1, 2)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Physical, Type.Normal, 7);
        return true;
    }
}