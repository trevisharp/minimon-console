namespace Minimon.Domain.Moves;

public class QuickTouchMove() : Move("Quick Touch", Type.Normal, 2, 2)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, Type.Normal, 8);
        ctx.Char.Bonus.AddPhysicalRecover(2, 2);
        return true;
    }
}