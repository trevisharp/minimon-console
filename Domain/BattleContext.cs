namespace Minimon.Domain;

public record BattleContext
{
    public required Creature Char { get; init; }
    public required Creature Enemy { get; init; }
}