using System;

namespace Minimon.Domain;

public class Events
{
    public Action? OnFaint;
    public Action? OnHeal;
    public Action? OnPhysicalShieldBreak;
    public Action? OnMagicalShieldBreak;
    public Action<int>? OnReceiveDamage;
    public Action<int>? OnReceivePhysicalDamage;
    public Action<int>? OnReceiveMagicalDamage;
    public Action<int>? OnReceiveRealDamage;
    public Func<int, int>? OnDealDamage;
    public Func<int, int>? OnDealPhysicalDamage;
    public Func<int, int>? OnDealMagicalDamage;
    public Func<int, int>? OnDealRealDamage;
    public Action<Effect>? OnReceiveEffect;
    public Action? OnTurn;
    public Action<Creature>? OnEnemyFind;
    public Action? OnUnsetup;
}