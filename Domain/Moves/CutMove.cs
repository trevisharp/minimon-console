namespace Minimon.Domain.Moves;

public class CutMove() : Move("Cut", Type.Normal)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, Type.Normal, 6);
        return true;
    }
}