namespace Minimon.Domain.Moves;

public class HardKickMove() : Move("Hard Kick", Type.Normal, -1, 2)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Physical, Type.Normal, 9);
        return true;
    }
}