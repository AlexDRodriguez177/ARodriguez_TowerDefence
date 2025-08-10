using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public Animator enemyAnimation;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string walkingBool = "IsWalking";
    [SerializeField] private int damageAmount;

    [SerializeField] private int maxHealth = 15;
    private int currentHealth;

    [SerializeField] private int coinsGivenToPlayer = 10;

    [SerializeField] public float baseSpeed;
    [SerializeField] private float currentSpeed;

    public float animationSpeed = .5f;
    private bool maxTowerAchived = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<Animator>();
        currentHealth = maxHealth;

        baseSpeed = agent.speed;
        currentSpeed = baseSpeed;
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
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachedEnd();
            }
        }

    }

    private void ReachedEnd()
    {
        enemyAnimation.SetBool(walkingBool, false);
        GameManager.Instance.playerHealth.TakeDamage(damageAmount);
        Destroy(gameObject);
    }

    public void EnemyTakeDamage(int incomingDamage)
    {
        currentHealth -= incomingDamage;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.isStopped = true;
            }
            Destroy(gameObject);
            GameManager.Instance.AddCoins(coinsGivenToPlayer);
        }
    }


    public void SetSpeed(float speed)
    {
        currentSpeed = speed;
        agent.speed = currentSpeed;
    }

    public void ResetSpeed()
    {
        SetSpeed(baseSpeed);
    }

    public void FreezeMaxTowerAchived(bool value)
    {
        maxTowerAchived = value;
    }

}

