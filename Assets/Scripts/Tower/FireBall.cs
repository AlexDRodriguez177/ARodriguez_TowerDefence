using UnityEngine;
using System.Collections;
public class FireBall : Projectile
{
    [SerializeField] private float explosionDelay = 1f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private int fireDamage = 15;
    private bool hasExploded = false;
    /// <summary>
    /// 
    /// </summary>
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
        foreach (var enemies in hits)
        {
            Enemy nearbyEnemy = enemies.GetComponent<Enemy>();
            if (nearbyEnemy != null)
            {
                Destroy(nearbyEnemy.gameObject);
            }
        }

        Destroy(gameObject);
    }
}
