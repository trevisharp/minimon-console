namespace Minimon.Domain;

public class Type
{
    public required string Name { get; init; }
    public required string[] AdvantageSet { get; init; }

    public static readonly Type Normal = Create(nameof(Normal),
        advantages: [ nameof(Magnetic), nameof(Gem), nameof(Air), nameof(Eletric) ]
    );

    public static readonly Type Aquatic = Create(nameof(Aquatic), 
        advantages: [ nameof(Fire), nameof(Normal), nameof(Air) ]
    );
    public static readonly Type Plant = Create(nameof(Plant),
        advantages: [ nameof(Aquatic), nameof(Normal) ]
    );
    public static readonly Type Fire = Create(nameof(Fire),
        advantages: [ nameof(Plant), nameof(Normal) ]
    );

    public static readonly Type Ice = Create(nameof(Ice),
        advantages: [ nameof(Fire), nameof(Rock), nameof(Normal) ]
    );
    public static readonly Type Rock = Create(nameof(Rock),
        advantages: [ nameof(Plant), nameof(Eletric) ]
    );
    public static readonly Type Eletric = Create(nameof(Eletric),
        advantages: [ nameof(Aquatic), nameof(Ice) ]
    );
    
    public static readonly Type Gem = Create(nameof(Gem),
        advantages: [ nameof(Ice), nameof(Eletric), nameof(Fire) ]
    );
    public static readonly Type Magnetic = Create(nameof(Magnetic),
        advantages: [ nameof(Eletric), nameof(Rock) ]
    );
    public static readonly Type Air = Create(nameof(Air),
        advantages: [ nameof(Rock), nameof(Ice) ]
    );

    static Type[]? types = null;
    public static Type[] Types => types ??= [ 
        Normal,
        Aquatic, Plant, Fire,
        Ice, Rock, Eletric,
        Gem, Magnetic, Air
    ];

    static Type Create(string name, string[]? advantages = null)
        => new() {
            Name = name,
            AdvantageSet = advantages ?? [],
        };
}