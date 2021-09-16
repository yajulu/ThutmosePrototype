using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    EnemyFieldOfView FOV_Object;
    [SerializeField]
    public Transform pathParent;
    [SerializeField]
    public Transform[] pathPoints;
    [SerializeField]
    LayerMask targetLayer;
    Animator enemyAnimator;
    int currPoint;
    NavMeshAgent agent;
    Transform playerTarget;

    //booleans
    bool isPatrolling, isSearching, isDetecting, isAttacking;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("EnemyPatrol", 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.remainingDistance <= 0 && isPatrolling)
        {
            if (!isSearching)
                StartCoroutine(EnemySearch());

           
        }

        if (FOV_Object.isPlayerDetected && (isPatrolling || isSearching))
        {
            isPatrolling = false;
            isSearching = false;
            if (!isDetecting)
            {
                StopAllCoroutines();
                StartCoroutine(StartDetecting());
                //Debug.Log("Hello");
                Debug.Log("Detectingg");
            }
                

        }

        if (isAttacking)
        {
            if (agent.remainingDistance < 2)
            {
                Debug.Log("Attacking");
                isAttacking = false;
                Collider[] playerCollier = Physics.OverlapSphere(transform.position, 2, targetLayer);
                if(playerCollier.Length > 0)
                {
                    Debug.Log("Player caught");
                }
                else
                {
                    Debug.Log("Player escaped");
                    isPatrolling = true;
                  
                }
            }
        }
    }


    void EnemyPatrol()
    {
        if (pathPoints.Length > 0)
        {
            isPatrolling = true;
            agent.SetDestination(pathPoints[currPoint].position);
        }
       

    }


    void AttackPlayer()
    {
        Debug.Log("Approaching");
        agent.isStopped = false;
        agent.updateRotation = true;
        agent.speed = agent.speed * 2.5f;
        agent.SetDestination(playerTarget.position);
        enemyAnimator.SetTrigger("Run");
        Debug.Log(agent.destination);
    }

    IEnumerator StartDetecting()
    {
        agent.updateRotation = false;
        transform.LookAt(playerTarget);
        
        agent.isStopped = true;
        isDetecting = true;
        yield return new WaitForSeconds(1);
        if (FOV_Object.isPlayerDetected)
        {
            isAttacking = true;
            AttackPlayer();
        }
        isDetecting = false;

    }


    IEnumerator EnemySearch()
    {
        isSearching = true;
        isPatrolling = false;
        enemyAnimator.SetTrigger("Search");
        yield return new WaitForSeconds(enemyAnimator.runtimeAnimatorController.animationClips[1].length);
        isSearching = false;
        isPatrolling = true;
        currPoint++;
        currPoint %= pathPoints.Length;
        EnemyPatrol();

    }
}
