namespace Minimon.Domain.Moves;

public class LightDewMove() : Move("Light Dew", Type.Plant, 0, 6)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        effect();

        var turn = ctx.Turn + 2;
        ctx.OnTurn += onTurn;

        return true;

        void onTurn()
        {
            if (ctx.Turn != turn)
                return;
            
            ctx.OnTurn -= onTurn;
            effect();
        }

        void effect()
        {
            ctx.Char.Bonus.ClearBonus();
            ctx.Enemy.Bonus.ClearBonus();
            ctx.Char.Heal(4);
            ctx.Enemy.Heal(4);
        }
    }
}