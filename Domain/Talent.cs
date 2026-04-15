namespace Minimon.Domain;

public abstract class Talent
{
    public abstract void Setup(Monster monster);

    public abstract void Unsetup(Monster monster);
}