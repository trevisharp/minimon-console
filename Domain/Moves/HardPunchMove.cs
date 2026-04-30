namespace Minimon.Domain.Moves;

public class HardPunchMove() : Move("Hard Punch", Type.Normal)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Physical, Type.Normal, 6);
        return true;
    }
}