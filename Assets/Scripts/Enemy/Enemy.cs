using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator enemyAnimation;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string walkingBool = "IsWalking";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<Animator>();
    }

    void Start()
    {
        enemyAnimation.SetBool(walkingBool, true);
    }

    public void Initialized(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                enemyAnimation.SetBool(walkingBool, false);
            }
        }

    }
}

