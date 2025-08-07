using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using System.Collections;
public class FreezeProjectile : Projectile
{
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float slowFactor = 0.5f;
    [SerializeField] private float slowDuration = 2f;
    private bool hasExploded = false;

    protected override void Update()
    {
        if (hasExploded)
            return;

        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            hasExploded = true;
            FreezeEnemies();
        }
    }

    private void FreezeEnemies()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.StartCoroutine(enemy.EnemyApplySlow(slowFactor, slowDuration));
            }
        }

        Destroy(gameObject);
    }
}
