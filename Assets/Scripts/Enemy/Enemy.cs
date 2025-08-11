using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    public Animator enemyAnimation;

    [SerializeField] protected Transform endPoint;
    [SerializeField] protected string walkingBool = "IsWalking";
    [SerializeField] protected int damageAmount;

    [SerializeField] protected int maxHealth = 15;
    protected int currentHealth;

    [SerializeField] protected int coinsGivenToPlayer = 10;

    [SerializeField] public float baseSpeed;
    [SerializeField] protected float currentSpeed;

    public float animationSpeed = .5f;
    protected bool maxTowerAchived = false;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<Animator>();
        currentHealth = maxHealth;

        baseSpeed = agent.speed;
        currentSpeed = baseSpeed;
    }

    protected virtual void Start()
    {
        enemyAnimation.SetBool(walkingBool, true);
    }

    public virtual void Initialized(Transform inputEndPoint)
    {
        endPoint = inputEndPoint;
        agent.SetDestination(endPoint.position);
    }

    protected virtual void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                ReachedEnd();
            }
        }
    }

    protected virtual void ReachedEnd()
    {
        enemyAnimation.SetBool(walkingBool, false);
        GameManager.Instance.playerHealth.TakeDamage(damageAmount);
        Destroy(gameObject);
    }

    public virtual void EnemyTakeDamage(int incomingDamage)
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

    public virtual void SetSpeed(float speed)
    {
        currentSpeed = speed;
        agent.speed = currentSpeed;
    }

    public virtual void ResetSpeed()
    {
        SetSpeed(baseSpeed);
    }

    public virtual void FreezeMaxTowerAchived(bool maxAchived)
    {
        maxTowerAchived = maxAchived;
    }
}

