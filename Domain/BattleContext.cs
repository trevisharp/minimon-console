namespace Minimon.Domain;

public record BattleContext
{
    public required Monster Char { get; init; }
    public required Monster Enemy { get; init; }
}