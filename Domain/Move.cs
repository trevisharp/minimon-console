namespace Minimon.Domain;

public abstract class Move(
    string name, Type type,
    int speed, int cost)
{
    public string Name { get; init; } = name;
    public Type Type { get; set; } = type;
    public int Speed { get; init; } = speed;
    public int Cost { get; init; } = cost;

    public void Use(BattleContext ctx)
    {
        if (!ctx.Char.SpendStamina(Cost))
            return;
        
        HandleMove(ctx);
    }

    protected abstract void HandleMove(BattleContext ctx);
}