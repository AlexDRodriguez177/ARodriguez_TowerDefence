using UnityEditor;
using UnityEngine;
using System.Collections;
public class EnemyStatusEffect : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private float immunityFromEffects = 2f;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public IEnumerator ApplyBurn(int damagePerTick, float duration, float tickRate)
    {
        float time = 0f;
        while (time < duration)
        {
            enemy.EnemyTakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickRate);
            time += tickRate;
        }
    }

    public IEnumerator ApplySlow(float slowFactor, float duration)
    {
        if (enemy.enemyAnimation != null)
        {
            enemy.enemyAnimation.speed *= slowFactor;
        }

        yield return new WaitForSeconds(duration);

        enemy.enemyAnimation.speed /= slowFactor;

    }
}
