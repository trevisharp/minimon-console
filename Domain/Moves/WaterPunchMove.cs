namespace Minimon.Domain.Moves;

public class WaterPunchMove() : Move("Water Punch", Type.Aquatic, 5, 1, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Real, 2);
        return true;
    }
}