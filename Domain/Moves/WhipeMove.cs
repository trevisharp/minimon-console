namespace Minimon.Domain.Moves;

public class WhipeMove() : Move("Whipe", Type.Plant, 3, 3)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Physical, Type.Plant, 7);
        return true;
    }
}