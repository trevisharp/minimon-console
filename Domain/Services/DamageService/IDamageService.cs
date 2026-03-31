namespace Minimon.Domain.Services.DamageService;

public interface IDamageService
{
    int ComputeDamage(Monster attacker, Monster defensor, Move move);
    int ApplyTypeAdvantage(int damage, Type damageType, Monster defensor);
}