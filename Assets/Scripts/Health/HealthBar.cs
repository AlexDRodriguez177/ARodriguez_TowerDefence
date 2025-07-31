using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image bar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (health != null)
        {
            health.OnHealthChanged += UpdateHealthBar;
        }
    }

    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        bar.fillAmount = (float)currentHealth / maxHealth;
    }
}
