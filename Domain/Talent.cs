namespace Minimon.Domain;

public abstract class Talent
{
    public abstract void Setup(Creature creature);

    public abstract void Unsetup(Creature creature);
}