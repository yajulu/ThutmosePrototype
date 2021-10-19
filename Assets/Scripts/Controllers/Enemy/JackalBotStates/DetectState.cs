using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectState : StateMachineBehaviour
{
    EnemyPathController enemyPathController;
    NavMeshAgent agent;
    Transform player;
    JackalBotController jackalBotController;
   
    Transform FakePlayerTransform;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyPathController = animator.GetComponent<EnemyPathController>();
        agent = animator.GetComponent<NavMeshAgent>();
        jackalBotController = animator.GetComponent<JackalBotController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.isStopped = true;
        jackalBotController.StartCoroutine(jackalBotController.DetectResult());
        if (jackalBotController.fovController.isFakePlayerDetected)
        {
            FakePlayerTransform = player.GetComponent<TimeAbilitiesController>().fakePlayer.transform;
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        agent.transform.LookAt(player);
        jackalBotController.AttackPlayer();

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackTrigger");
        agent.isStopped = false;
    }

  
}
