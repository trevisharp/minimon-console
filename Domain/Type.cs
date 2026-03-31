namespace Minimon.Domain;

public class Type
{
    public required string Name { get; init; }
    public required string[] HighAdvantageSet { get; init; }
    public required string[] AdvantageSet { get; init; }
    public required string[] WeakSet { get; init; }
    public required string[] HighWeakSet { get; init; }

    public static readonly Type Normal = Create("Normal");

    public static readonly Type Fire = Create("Fire",
        highadvgAgainst: [ "Plant" ]
    );
    public static readonly Type Water = Create("Water", 
        highadvgAgainst: [ "Fire" ]
    );
    public static readonly Type Plant = Create("Plant",
        highadvgAgainst: [ "Water" ]
    );

    public static readonly Type Eletric = Create("Eletric",
        advgAgainst: [ "Ice" ], 
        highweakAgainst: [ "Eletric" ]
    );
    public static readonly Type Rock = Create("Rock",
        advgAgainst: [ "Eletric" ],
        highweakAgainst: [ "Rock" ]
    );
    public static readonly Type Ice = Create("Ice",
        advgAgainst: [ "Rock" ],
        highweakAgainst: [ "Ice" ]
    );

    public static readonly Type Dragon = Create("Dragon",
        advgAgainst: [ "Plant", "Ice" ], 
        weakAgainst: [ "Fairy", "Ghost" ]
    );
    public static readonly Type Ghost = Create("Ghost",
        advgAgainst: [ "Fire", "Eletric" ],
        weakAgainst: [ "Dragon", "Fairy" ]
    );
    public static readonly Type Fairy = Create("Fairy",
        advgAgainst: [ "Water", "Rock" ],
        weakAgainst: [ "Dragon", "Ghost" ]
    );

    static Type[]? types = null;
    public static Type[] Types => types ??= [ 
        Normal,
        Fire, Water, Plant,
        Eletric, Rock, Ice,
        Dragon, Ghost, Fairy
    ];

    static Type Create(string name, 
        string[]? highadvgAgainst = null, string[]? advgAgainst = null, 
        string[]? weakAgainst = null, string[]? highweakAgainst = null)
        => new()
        {
            Name = name,
            HighAdvantageSet = highadvgAgainst ?? [],
            AdvantageSet = advgAgainst ?? [],
            WeakSet = weakAgainst ?? [],
            HighWeakSet = highweakAgainst ?? []
        };
}