using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunState : StateMachineBehaviour
{
    JackalBotController jackalBotController;
    NavMeshAgent agent;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jackalBotController = animator.GetComponent<JackalBotController>();
        agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        jackalBotController.fovController.isDetectingStopped = true;
        jackalBotController.particles.Play();
        jackalBotController.StartCoroutine(jackalBotController.Stun());
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jackalBotController.fovController.isDetectingStopped = false;
        jackalBotController.particles.Stop();
        agent.isStopped = false;
        
    }

   
}
