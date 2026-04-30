namespace Minimon.Domain.Moves;

public class TwistArmMove() : Move("Twist Arm", Type.Normal, -4, 4)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        int bestBonus = int.Max(
            ctx.Enemy.Bonus.PhysicalDamageBonus,
            ctx.Enemy.Bonus.MagicalDamageBonus
        );
        var damage = int.Max(bestBonus, 0) + 1;
        
        ctx.Enemy.Bonus.AddLifeRecover(-damage, 3);
        return true;
    }
}