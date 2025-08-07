using TMPro.EditorUtilities;
using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(SphereCollider))]
public abstract class Tower : MonoBehaviour
{
    public float fireCooldown = 8.0f;

    protected float currentFireCooldown = 0.0f;
    protected List<Enemy> enemiesInRange = new List<Enemy>();

    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform towerWeaponRotate;
    [SerializeField] protected float rotationSpeed = 10f;
    [SerializeField] protected Transform arrowSpawnPoint;


    protected virtual void Update()
    {
        currentFireCooldown -= Time.deltaTime;
        if (currentFireCooldown < 0f)
        {
            currentFireCooldown = 0f;
        }


        Enemy closestEnemy = GetClosestEnemy();
        if (closestEnemy != null)
        {
            RotateTowerWeapon(closestEnemy.transform);
            if (closestEnemy != null && currentFireCooldown <= 0.0f)
            {

                FireAt(closestEnemy);
                currentFireCooldown = fireCooldown;

            }
        }


    }

    protected virtual void RotateTowerWeapon(Transform enemy)
    {
        Vector3 targetPosition = enemy.position - towerWeaponRotate.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition);
        towerWeaponRotate.rotation = Quaternion.Slerp(towerWeaponRotate.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    protected virtual void FireAt(Enemy target)
    {
        if (projectilePrefab != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            projectileInstance.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    private Enemy GetClosestEnemy()
    {

        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null)
            {
                enemiesInRange.RemoveAt(i);
            }
        }
        Enemy closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (Enemy enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
    }


}
