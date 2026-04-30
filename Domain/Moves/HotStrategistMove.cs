namespace Minimon.Domain.Moves;

public class HotStrategistMove() : Move("Hot Strategist", Type.Fire, 4, 8)
{
    protected override bool HandleMove(BattleContext ctx)
    {
        var user = ctx.Char;
        
        ctx.RequestSwap();
        ctx.Enemy.Recive(DamageType.Weak, Type.Normal, 6);

        var turn = ctx.Turn + 1;
        ctx.OnTurn += onTurn;

        return true;

        void onTurn()
        {
            if (ctx.Turn != turn)
                return;
            
            ctx.OnTurn -= onTurn;
            ctx.RequestReturn(user);
        }
    }
}