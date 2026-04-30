namespace Minimon.Domain.Moves;

public class FireBreathMove() : Move("Fire Breath", Type.Fire, 3, 3)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        ctx.Enemy.Recive(DamageType.Weak, Type.Fire, 10);
        return true;
    }
}