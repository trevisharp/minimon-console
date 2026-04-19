namespace Minimon.Domain.Talents;

public class OverSkillTalent : Talent
{
    public override void Setup(Creature creature)
    {
        this.creature = creature;
        creature.OnEnemyFind += OnEnemyFind;
        creature.OnTurn += OnTurn;
    }

    public override void Unsetup(Creature creature)
    {
        this.creature = null;
        creature.OnEnemyFind -= OnEnemyFind;
        creature.OnTurn -= OnTurn;
    }

    Creature? creature, enemy;
    
    void OnEnemyFind(Creature enemy)
        => this.enemy = enemy;
    
    void OnTurn()
    {
        if (creature is null || enemy is null)
            return;

        if (creature.Ability < enemy.Ability)
            return;
        
        creature.ClearEffect();
        creature.Apply(Effect.Confident);
    }
}