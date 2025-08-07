using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator enemyAnimation;
    [SerializeField] private Transform endPoint;
    [SerializeField] private string walkingBool = "IsWalking";
    [SerializeField] private int damageAmount;

    [SerializeField] private int maxHealth = 15;
    private int currentHealth;

    [SerializeField] public float baseSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float immunityFromEffects = 2f;

    private float animationSpeed = .5f;
    private bool isBurning = false;
    private bool canBeSlowedAgain = true;
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

    // Update is called once per frame
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
        }
    }

    public IEnumerator ApplyBurn(int burnDamage, float burnDuration, float burnTickRate)
    {
        if (isBurning)
        {
            yield break;
        }
        isBurning = true;
        float elapsedTime = 0f;

        while (elapsedTime < burnDuration)
        {
            EnemyTakeDamage(burnDamage);
            elapsedTime += burnTickRate;
            yield return new WaitForSeconds(burnTickRate);
        }
        isBurning = false;
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

    public IEnumerator EnemyApplySlow(float slowSpeed, float duration)
    {
        if (!canBeSlowedAgain)
        {
            yield break;
        }
        canBeSlowedAgain = false;
        SetSpeed(baseSpeed * slowSpeed);
        if (maxTowerAchived)
        {
            enemyAnimation.SetBool(walkingBool, false);
        }
        else
        {
            enemyAnimation.SetBool(walkingBool, true);
            enemyAnimation.speed = animationSpeed;
        }
        yield return new WaitForSeconds(duration);
        ResetSpeed();
        yield return new WaitForSeconds(immunityFromEffects);
        enemyAnimation.speed = 1f;
        canBeSlowedAgain = true;
    }
}

