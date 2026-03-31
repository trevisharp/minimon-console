namespace Minimon.Domain;

public abstract class Move(
    string name, MoveType moveType, 
    Type type, int baseDamage, 
    MovePriority priority)
{
    public string Name { get; init; } = name;
    public MoveType MoveType { get; init; } = moveType;
    public MovePriority Priority { get; init; } = priority;
    public Type Type { get; set; } = type;
    public int BaseDamage { get; set; } = baseDamage;
}