using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 3f;
    private Transform target;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target != null)
        {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction; 
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform==target)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                
                Destroy(enemy.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
