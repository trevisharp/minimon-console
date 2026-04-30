namespace Minimon.Domain.Moves;

public class ScratchMove() : Move("Scratch", Type.Normal)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Strong, Type.Normal, 3);
        return true;
    }
}