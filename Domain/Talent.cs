using System;

namespace Minimon.Domain;

public abstract class Talent
{
    public abstract void Setup(Creature creature);

    public abstract void Unsetup(Creature creature);
}

public class LambdaTalent(Action<Creature, Events> setup) : Talent
{
    Events? events = null;
    void Load(Creature creature)
    {
        if (events is not null)
            return;

        events = new();
        setup(creature, events);    
    }

    public override void Setup(Creature creature)
    {
        Load(creature);
        if (events is null)
            return;
        
        creature.OnFaint += events.OnFaint;
        creature.OnHeal += events.OnHeal;
        creature.OnPhysicalShieldBreak += events.OnPhysicalShieldBreak;
        creature.OnMagicalShieldBreak += events.OnMagicalShieldBreak;
        creature.OnReceiveDamage += events.OnReceiveDamage;
        creature.OnReceivePhysicalDamage += events.OnReceivePhysicalDamage;
        creature.OnReceiveMagicalDamage += events.OnReceiveMagicalDamage;
        creature.OnReceiveRealDamage += events.OnReceiveRealDamage;
        creature.OnDealDamage += events.OnDealDamage;
        creature.OnDealPhysicalDamage += events.OnDealPhysicalDamage;
        creature.OnDealMagicalDamage += events.OnDealMagicalDamage;
        creature.OnDealRealDamage += events.OnDealRealDamage;
        creature.OnReceiveEffect += events.OnReceiveEffect;
        creature.OnTurn += events.OnTurn;
        creature.OnEnemyFind += events.OnEnemyFind;
    }

    public override void Unsetup(Creature creature)
    {
        Load(creature);
        if (events is null)
            return;
        
        creature.OnFaint += events.OnFaint;
        creature.OnHeal += events.OnHeal;
        creature.OnPhysicalShieldBreak += events.OnPhysicalShieldBreak;
        creature.OnMagicalShieldBreak += events.OnMagicalShieldBreak;
        creature.OnReceiveDamage += events.OnReceiveDamage;
        creature.OnReceivePhysicalDamage += events.OnReceivePhysicalDamage;
        creature.OnReceiveMagicalDamage += events.OnReceiveMagicalDamage;
        creature.OnReceiveRealDamage += events.OnReceiveRealDamage;
        creature.OnDealDamage += events.OnDealDamage;
        creature.OnDealPhysicalDamage += events.OnDealPhysicalDamage;
        creature.OnDealMagicalDamage += events.OnDealMagicalDamage;
        creature.OnDealRealDamage += events.OnDealRealDamage;
        creature.OnReceiveEffect += events.OnReceiveEffect;
        creature.OnTurn += events.OnTurn;
        creature.OnEnemyFind += events.OnEnemyFind;
    }

}