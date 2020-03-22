using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    public static void AddHealth()
    {
        PlayerStats.Instance.AddHealth();
    }

    public static void Heal(float health)
    {
        PlayerStats.Instance.Heal(health);
    }

    public static void Hurt(float dmg)
    {
        PlayerStats.Instance.TakeDamage(dmg);
    }
}
