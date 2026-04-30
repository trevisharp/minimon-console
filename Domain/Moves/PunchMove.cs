namespace Minimon.Domain.Moves;

public class PunchMove() : Move("Punch", Type.Normal)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Physical, Type.Normal, 4);
        return true;
    }
}