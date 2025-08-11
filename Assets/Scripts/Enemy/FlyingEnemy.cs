using System.Net;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    [SerializeField] private float flyingSpeed = 5f;
    [SerializeField] private float flyingHeight = 5f;
    [SerializeField] private float rotationSpeed = 50f;
    protected override void Awake()
    {    
        base.Awake();
        if (agent != null)
        {
            agent.enabled = false;
            agent = null;
        }
    }
    public override void Initialized(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
    }

    protected override void Update()
    {
        if (endPoint == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, flyingSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndPoint"))
        {
            ReachedEnd();
        }
    }
}
