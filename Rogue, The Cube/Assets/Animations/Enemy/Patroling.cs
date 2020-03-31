using UnityEngine;
using UnityEngine.AI;

public class Patroling : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Vector3 spawnLocation;
    private float patrolRadius = 20f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = animator.GetComponent<Stats>().MOVESPEED.GetValue();

        Vector3 newPosition = animator.transform.parent.position + new Vector3(UnityEngine.Random.Range(-patrolRadius, patrolRadius), animator.transform.position.y, UnityEngine.Random.Range(-patrolRadius, patrolRadius));

        agent.SetDestination(newPosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= 2f)
        {
            animator.SetBool("isPatroling", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}