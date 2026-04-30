using System.Collections.Generic;

namespace Minimon.Domain;

public class Bonus
{
    int turn = 0;
    readonly List<(string name, int value, int end)> bonus = [];
    public int PhysicalDamageBonus { get; private set; } = 0;
    public int MagicalDamageBonus { get; private set; } = 0;
    public int PhysicalRecover { get; private set; } = 0;
    public int MagicalRecover { get; private set; } = 0;

    public void AddPhysicalDamage(int value, int duration)
        => Add(nameof(PhysicalDamageBonus), value, turn + duration);

    public void AddMagicalDamage(int value, int duration)
        => Add(nameof(MagicalDamageBonus), value, turn + duration);

    public void AddPhysicalRecover(int value, int duration)
        => Add(nameof(PhysicalRecover), value, turn + duration);

    public void AddMagicalRecover(int value, int duration)
        => Add(nameof(MagicalRecover), value, turn + duration);
    
    public void CommitTurn()
    {
        turn++;
        for (int i = 0; i < bonus.Count; i++)
        {
            var (name, value, end) = bonus[i];
            if (turn < end)
                continue;
            
            bonus.RemoveAt(i);
            i--;
            Remove(name, value);
        }
    }
    
    public void ClearBonus()
    {
        turn = 0;
        bonus.Clear();
        PhysicalDamageBonus = 0;
        PhysicalRecover = 0;
        MagicalDamageBonus = 0;
        MagicalRecover = 0;
    }

    void Add(string name, int value, int end)
    {
        bonus.Add((name, value, end));
        foreach (var prop in typeof(Bonus).GetProperties())
        {
            if (prop.Name != name)
                continue;
            var propVal = prop.GetValue(this) ?? 0;
            
            prop.SetValue(this, (int)propVal + value);
        }
    }

    void Remove(string name, int value)
    {
        foreach (var prop in typeof(Bonus).GetProperties())
        {
            if (prop.Name != name)
                continue;
            var propVal = prop.GetValue(this) ?? 0;
            
            prop.SetValue(this, (int)propVal - value);
        }
    }
}