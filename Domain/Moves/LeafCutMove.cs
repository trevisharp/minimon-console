namespace Minimon.Domain.Moves;

public class LeafCutMove() : Move("Leaf Cut", Type.Plant, 7, 3, 1)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Strong, 3);
        return true;
    }
}