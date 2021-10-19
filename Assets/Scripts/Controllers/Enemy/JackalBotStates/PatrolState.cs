using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{

    EnemyPathController enemyPathController;
    NavMeshAgent agent;
    JackalBotController jackalBotController;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyPathController = animator.GetComponent<EnemyPathController>();
        agent = animator.GetComponent<NavMeshAgent>();
        jackalBotController = animator.GetComponent<JackalBotController>();
        agent.isStopped = false;
        agent.speed = jackalBotController.patrolSpeed;
        enemyPathController.GoToNextPoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= 0)
            animator.SetTrigger("LookAroundTrigger");
        jackalBotController.AttackPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("LookAroundTrigger");
        animator.ResetTrigger("AttackTrigger");
    }

    
}
