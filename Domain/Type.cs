namespace Minimon.Domain;

public class Type
{
    public required string Name { get; init; }
    public required string[] AdvantageSet { get; init; }

    public static readonly Type Normal = Create("Normal",
        advantages: [ "Magnetic", "Air", "Dark" ]
    );

    public static readonly Type Magma = Create("Magma",
        advantages: [ "Plant", "Normal" ]
    );
    public static readonly Type Aquatic = Create("Aquatic", 
        advantages: [ "Magma", "Normal" ]
    );
    public static readonly Type Plant = Create("Plant",
        advantages: [ "Aquatic", "Normal" ]
    );

    public static readonly Type Eletric = Create("Eletric",
        advantages: [ "Aquatic", "Ice" ]
    );
    public static readonly Type Rock = Create("Rock",
        advantages: [ "Plant", "Eletric" ]
    );
    public static readonly Type Ice = Create("Ice",
        advantages: [ "Magma", "Rock" ]
    );

    public static readonly Type Magnetic = Create("Magnetic",
        advantages: [ "Eletric", "Rock" ]
    );
    public static readonly Type Air = Create("Air",
        advantages: [ "Rock", "Ice" ]
    );
    public static readonly Type Dark = Create("Dark",
        advantages: [ "Ice", "Eletric" ]
    );

    static Type[]? types = null;
    public static Type[] Types => types ??= [ 
        Normal,
        Magma, Aquatic, Plant,
        Eletric, Rock, Ice,
        Magnetic, Air, Dark
    ];

    static Type Create(string name, string[]? advantages = null)
        => new() {
            Name = name,
            AdvantageSet = advantages ?? [],
        };
}