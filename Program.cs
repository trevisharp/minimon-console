using Minimon.Domain;

var creature = Creature.FromSpecies(Indexer.Simeolil);
creature.EarnExperience(300_000);
for (int i = 0; i < 8; i++)
    creature.Upgrade(Status.Life);
for (int i = 0; i < 4; i++)
creature.Upgrade(Status.MagicalDefense);

print(creature);

creature.Heal();
print(creature);

creature.Evolve();
print(creature);

creature.Evolve();
print(creature);

creature.Heal();
print(creature);

void print(Creature creature)
{
    System.Console.WriteLine(
        $"""
        {creature.Species.Name}
        HP: {creature.CurrentLife} / {creature.Life}=
        PS: {creature.CurrentPhysicalShield} / {creature.PhysicalDefense}
        MS: {creature.CurrentMagicalShield} / {creature.MagicalDefense}
        """
    );
}
