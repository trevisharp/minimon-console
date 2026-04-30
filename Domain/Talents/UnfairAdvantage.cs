namespace Minimon.Domain.Talents;

public class UnfairAdvantage() : LambdaTalent(
    (creature, evs) =>
    {
        Creature? enemy = null;
        evs.OnEnemyFind += en => enemy = en;

        evs.OnDealDamage += damage =>
        {
            if (enemy is null)
                return damage;
            
            if (!creature.HasAdvantage(enemy))
                return damage;
            
            return damage + 2;
        };

        evs.OnUnsetup += () => enemy = null;
    }
);