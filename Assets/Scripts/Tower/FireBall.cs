using UnityEngine;
using System.Collections;
public class FireBall : Projectile
{
    [SerializeField] private float explosionDelay = 1f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private int burnDamage = 2;
    [SerializeField] private float burnDuration = 4f;
    [SerializeField] private float burnTickRate = 1f;
    private bool hasExploded = false;

    protected override void Update()
    {
        if (hasExploded)
        {
            return;
        }
        base.Update();
    }
    /// <summary>
    /// If the fireball collides with the target, it will trigger an explosion after a delay.
    /// Starts a coroutine to handle the explosion delay.
    /// </summary>
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            hasExploded = true;
            StartCoroutine(ExplodeAfterDelay());
        }
    }
    /// <summary>
    /// Adds a delay before the explosion occurs, allowing the fireball to travel a bit before exploding.
    /// Uses a coroutine to handle the delay.
    /// Addeds enemies within the explosion radius to a list and destroys them.
    /// Will add damage when enmies have a proper health system.
    /// </summary>
    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(explosionDelay);

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            EnemyStatusEffect statusHandler = hit.GetComponent<EnemyStatusEffect>();
            if (statusHandler != null)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.EnemyTakeDamage(damage);
                }
                statusHandler.StartCoroutine(statusHandler.ApplyBurn(burnDamage, burnDuration, burnTickRate));
            }
        }

        Destroy(gameObject);
    }
}
