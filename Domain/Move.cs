namespace Minimon.Domain;

public abstract class Move(
    string name, Type type,
    int speed = 0,
    int cooldown = 0)
{
    public string Name { get; init; } = name;
    public Type Type { get; set; } = type;
    public int Speed { get; init; } = speed;
    public int Cooldown { get; init; } = cooldown;
    public int CurrentCooldown { get; private set; } = 0;

    public bool Use(BattleContext ctx)
    {
        if (CurrentCooldown > 0)
            return false;
        
        CurrentCooldown = Cooldown;
        return HandleMove(ctx);
    }

    public void Reset()
    {
        CurrentCooldown = 0;
        HandleReset();
    }

    protected abstract bool HandleMove(BattleContext ctx);

    protected virtual void HandleReset() { }
}