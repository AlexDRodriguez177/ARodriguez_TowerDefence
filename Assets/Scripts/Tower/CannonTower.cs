using UnityEngine;

public class CannonTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }
}
