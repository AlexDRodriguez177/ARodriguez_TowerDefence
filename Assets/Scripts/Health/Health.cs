using UnityEngine;

public class Health : MonoBehaviour
{
    public event System.Action <int, int> OnHealthChanged;

    [SerializeField] private int maxHealth = 20;
    private int currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
    public bool IsDead()
    {
        return currentHealth > 0;
    }
    public void TakeDamage(int damageAmmount)
    {
        if (currentHealth > 0)
        {

            currentHealth = Mathf.Max(currentHealth - damageAmmount, 0);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

    }
}
