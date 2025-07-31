using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float speed = 20f;
    [SerializeField] protected float lifetime = 3f;
    protected private Transform target;
    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    protected virtual void Update()
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

    public virtual void SetTarget(Transform inputTarget)
    {
        target = inputTarget;
    }

    protected virtual void OnTriggerEnter(Collider other)
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
