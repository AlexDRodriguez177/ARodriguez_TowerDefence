using UnityEngine;
using System.Collections;
public class BladeTower : MonoBehaviour
{
    [SerializeField] private int bladeDamage;
    [SerializeField] private float timeBeforeNextStrike = 1f;
    private bool canDealDamage = true;
    private void OnTriggerEnter(Collider other)
    {
        if (!canDealDamage)
        {
            return;
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.EnemyTakeDamage(bladeDamage);
            StartCoroutine(CoolDownTimer());
        }
    }

    private IEnumerator CoolDownTimer()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(timeBeforeNextStrike);
        canDealDamage = true;
    }
}
