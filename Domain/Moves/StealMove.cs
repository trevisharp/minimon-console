namespace Minimon.Domain.Moves;

public class StealMove() : Move("Steal", Type.Normal)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, Type.Normal, 6);
        ctx.Char.Heal(2);
        return true;
    }
}