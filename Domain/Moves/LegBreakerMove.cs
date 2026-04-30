namespace Minimon.Domain.Moves;

public class LegBreakerMove() : Move("Leg Breaker", Type.Normal, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Bonus.ClearBonus();
        ctx.Enemy.Recive(DamageType.Physical, Type.Normal, 2);
        return true;
    }
}