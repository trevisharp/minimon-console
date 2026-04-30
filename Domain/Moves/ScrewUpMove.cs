namespace Minimon.Domain.Moves;

public class ScrewUpMove() : Move("Screw Up", Type.Normal, -1, 2)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, Type.Normal, 9);
        return true;
    }
}