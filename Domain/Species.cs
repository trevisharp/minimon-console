namespace Minimon.Domain;

public class Species
{
    public required string Name { get; init; }
    public required Type MainType { get; init; }
    public required Type? SecondType { get; init; }
    public required Species? Evolution { get; init; }
    public required Talent Talent { get; init; }
    public required int BaseLife { get; init; }
    public required int BasePhysicalDefense { get; init; }
    public required int BaseMagicalDefense { get; init; }
    public required int SpeedIndex { get; init; }
    public required MoveSet MoveSet { get; init; }
}