using UnityEngine;

public class BalistaTower : Tower
{
    protected override void Update()
    {
        base.Update();
    }
    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

}
