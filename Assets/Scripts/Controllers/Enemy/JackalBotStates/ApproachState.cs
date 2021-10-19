using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ApproachState : StateMachineBehaviour
{
    
    NavMeshAgent agent;
    Transform player;
    JackalBotController jackalBotController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        jackalBotController = animator.GetComponent<JackalBotController>();
        agent.isStopped = false;
        agent.speed = jackalBotController.approachSpeed;
        if (jackalBotController.fovController.isFakePlayerDetected)
        {
            agent.SetDestination(player.GetComponent<TimeAbilitiesController>().fakePlayer.transform.position);
        }
        else
            agent.SetDestination(player.position);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jackalBotController.AttackPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackTrigger");
    }

  
}
