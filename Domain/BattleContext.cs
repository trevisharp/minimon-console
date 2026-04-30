using System;

namespace Minimon.Domain;

public class BattleContext(Creature fst, Creature snd)
{
    public Creature Char { get; private set; } = fst;
    public Creature Enemy { get; private set; } = snd;
    public int Turn { get; private set; } = 0;

    public event Action? OnTurn;

    public void StartTurn()
    {
        Turn++;
        OnTurn?.Invoke();
    }
}