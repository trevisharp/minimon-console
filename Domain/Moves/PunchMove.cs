namespace Minimon.Domain.Moves;

public class PunchMove() : Move("Punch", Type.Normal, 6, 2, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, 4);
        return true;
    }
}