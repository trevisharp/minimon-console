namespace Minimon.Domain.Moves;

public class WhipeMove() : Move("Whipe", Type.Plant, 7, 3, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, 6);
        return true;
    }
}