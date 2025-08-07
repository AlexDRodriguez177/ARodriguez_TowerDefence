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

    private bool isBurning = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimation = GetComponent<Animator>();
        currentHealth = maxHealth;
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
}

