namespace Minimon.Domain;

public class Species
{
    public required string Name { get; init; }
    public required Type MainType { get; init; }
    public required Type SecondType { get; init; }
    public required int BaseLife { get; init; }
    public required int BaseDefense { get; init; }
    public required int BaseSpeed { get; init; }
    public required int BaseStrength { get; init; }
    public required int BaseAgility { get; init; }
    public required int BaseInteligence { get; init; }
}