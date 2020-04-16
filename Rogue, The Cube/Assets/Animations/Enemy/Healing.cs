using UnityEngine;
//using UnityEngine.AI;

public class Healing : StateMachineBehaviour
{
    Controller_AI c_ai;
    //NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        c_ai = animator.GetComponent<Controller_AI>();
        //agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (c_ai.mainTarget != null)
        {

            if (Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) < c_ai.attackingDistance)
            {
                if (animator.GetComponentInChildren<Controller_Offhand>())
                {
                    animator.GetComponentInChildren<Controller_Offhand>().target = c_ai.mainTarget;
                    animator.GetComponentInChildren<Controller_Offhand>().UseOffhand();
                }

                if (c_ai.mainTarget.GetComponentInParent<Stats>().CurrentHealth >= c_ai.mainTarget.GetComponentInParent<Stats>().HITPOINTS.GetValue() * 0.8f)
                {
                    c_ai.mainTarget = null;
                    animator.SetTrigger("isReseting");
                }
            }
            else
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, c_ai.mainTarget.transform.position, 1 * Time.deltaTime);
            }
        }
        else if (c_ai.mainTarget == null)
        {
            animator.SetTrigger("isReseting");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        c_ai.isEngaged = false;
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
