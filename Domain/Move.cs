namespace Minimon.Domain;

public abstract class Move(
    string name, Type type,
    int speed, int cost)
{
    public string Name { get; init; } = name;
    public Type Type { get; set; } = type;
    public int Speed { get; init; } = speed;
    public int Cost { get; init; } = cost;

    public bool Use(BattleContext ctx)
    {
        var realCost = ctx.Char.CurrentEffect switch
        {
            Effect.Confident => int.Max(Cost - 2, 0),
            _ => Cost  
        };
        if (!ctx.Char.SpendStamina(realCost))
            return false;
        
        return HandleMove(ctx);
    }

    protected abstract bool HandleMove(BattleContext ctx);
}