namespace Minimon.Domain.Moves;

public class SwapMove() : Move("Swap", Type.Normal, 4, 4)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, Type.Normal, 4);
        ctx.RequestSwap();
        return true;
    }
}