namespace Minimon.Domain;

public abstract class Move(
    string name, Type type,
    int speed, int cost,
    int cooldown)
{
    public string Name { get; init; } = name;
    public Type Type { get; set; } = type;
    public int Speed { get; init; } = speed;
    public int Cost { get; init; } = cost;
    public int Cooldown { get; init; } = cooldown;
    public int CurrentCooldown { get; private set; } = 0;

    public bool Use(BattleContext ctx)
    {
        if (CurrentCooldown > 0)
            return false;

        var realCost = ctx.Char.CurrentEffect switch
        {
            Effect.Confident => int.Max(Cost - 2, 0),
            _ => Cost  
        };
        if (!ctx.Char.SpendStamina(realCost))
            return false;
        
        CurrentCooldown = Cooldown;
        return HandleMove(ctx);
    }

    protected abstract bool HandleMove(BattleContext ctx);
}