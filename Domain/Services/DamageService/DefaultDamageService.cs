using System;

namespace Minimon.Domain.Services.DamageService;

public class DefaultDamageService : IDamageService
{
    public int ApplyTypeAdvantage(int damage, Type damageType, Monster defensor)
    {
        var typeA = defensor.Species.MainType.Name;
        var typeB = defensor.Species.SecondType.Name;
        
        var set = damageType.HighAdvantageSet;
        if (set.Contains(typeA) || set.Contains(typeB))
            return 2 * damage;

        set = damageType.AdvantageSet;
        if (set.Contains(typeA) || set.Contains(typeB))
            return 3 * damage / 2;
        
        set = damageType.WeakSet;
        if (set.Contains(typeA) || set.Contains(typeB))
            return damage / 2;
        
        set = damageType.HighWeakSet;
        if (set.Contains(typeA) || set.Contains(typeB))
            return damage / 4;
        
        return damage;
    }

    public int ComputeDamage(Monster attacker, Monster defensor, Move move)
    {
        var damage = ComputeDamage(attacker, move);
        var shield = ComputeShield(defensor, move);

        var randDam = 10 - Random.Shared.Next(21);
        var inflicts = damage + randDam - shield;

        inflicts = ApplyTypeAdvantage(inflicts, move.Type, defensor);
        if (inflicts < 0)
            return 0;
        
        return inflicts;
    }

    int ComputeShield(Monster defensor, Move move)
    {
        var monsterBasedDamange = move.MoveType switch
        {
            MoveType.Physical => defensor.Inteligence,
            MoveType.Technical => defensor.Agility,
            MoveType.Agile => defensor.Strength,
            _ => 0  
        };

        return defensor.Defense / 2 + monsterBasedDamange / 4;
    }

    int ComputeDamage(Monster attacker, Move move)
    {
        var monsterBasedDamange = move.MoveType switch
        {
            MoveType.Physical => attacker.Strength + attacker.Agility,
            MoveType.Technical => attacker.Strength + attacker.Inteligence,
            MoveType.Agile => attacker.Agility + attacker.Inteligence,
            _ => 0  
        };

        return move.BaseDamage + monsterBasedDamange;
    }
}