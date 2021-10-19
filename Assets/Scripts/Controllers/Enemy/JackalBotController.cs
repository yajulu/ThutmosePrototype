using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JackalBotController : MonoBehaviour
{
    [HideInInspector]
    public EnemyFieldOfView fovController;
    Animator animator;
    NavMeshAgent agent;
    bool isPlayerCurrentlyDetected;
    [HideInInspector]
    public ParticleSystem particles;
    [SerializeField]
    public float detectionTimeWindow,patrolSpeed,approachSpeed,stunTime;
    Transform player;
    [SerializeField]
    LayerMask targetLayer;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        fovController = GetComponentInChildren<EnemyFieldOfView>();
        particles = GetComponentInChildren<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerAnimationHandler();
    }


    void DetectPlayerAnimationHandler()
    {
        if ((fovController.isFakePlayerDetected || fovController.isPlayerDetected) && !isPlayerCurrentlyDetected)
        {
            animator.SetTrigger("DetectTrigger");
            isPlayerCurrentlyDetected = true;
        }
        if ((!fovController.isFakePlayerDetected && !fovController.isPlayerDetected) && isPlayerCurrentlyDetected)
        {
            animator.ResetTrigger("DetectTrigger");
            isPlayerCurrentlyDetected = false;
        }
    }

    public IEnumerator DetectResult()
    {
        yield return new WaitForSeconds(detectionTimeWindow);
        if (fovController.isPlayerDetected || fovController.isFakePlayerDetected)
        {
            animator.SetTrigger("ApproachTrigger");
            animator.ResetTrigger("DetectTrigger");
        }
        else
        {
            animator.SetTrigger("PatrolTrigger");
            animator.ResetTrigger("DetectTrigger");
        }
    }
    public IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunTime);
        animator.SetTrigger("LookAroundTrigger");
    }

    public void AttackPlayer()
    {
        
        if(agent.remainingDistance < 2)
        {
            Collider[] playerCollier = Physics.OverlapSphere(transform.position, 1.5f, targetLayer);
            if (playerCollier.Length > 0)
            {
                agent.isStopped = true;
                animator.SetTrigger("AttackTrigger");
                if (playerCollier[0].CompareTag("FakePlayer"))
                    playerCollier[0].transform.GetChild(0).GetComponent<Animator>().SetTrigger("DissolveTrig");
            }
            else
            {
                animator.SetTrigger("LookAroundTrigger");
            }
           

        }
    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stone"))
        {
            animator.SetTrigger("HitTrigger");
            other.GetComponent<StoneController>().DestroyStoneOnEnemyHit();
        }
       
    }
}
