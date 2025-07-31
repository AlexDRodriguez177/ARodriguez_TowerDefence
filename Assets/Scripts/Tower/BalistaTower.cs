using UnityEngine;

public class BalistaTower : Tower
{
    [SerializeField] private GameObject projectilePrefab;

    protected override void Update()
    {
        base.Update();
    }
    protected override void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectileInstance.GetComponent<Projectle>().SetTarget(target.transform);
        }
    }

}
