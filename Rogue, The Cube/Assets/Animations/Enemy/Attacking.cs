using UnityEngine;
using UnityEngine.AI;

public class Attacking : StateMachineBehaviour
{
    Controller_AI c_ai;
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        c_ai = animator.GetComponent<Controller_AI>();
        agent = animator.GetComponent<NavMeshAgent>();

        if (c_ai.mainTarget != null)
            agent.SetDestination(c_ai.mainTarget.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (c_ai.mainTarget != null)
        {
            agent.SetDestination(c_ai.mainTarget.transform.position);

            if (Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) <= c_ai.attackingDistance)
            {
                LookAt(c_ai.mainTarget.gameObject);

                if (ForwardRay("Player"))
                {
                    agent.isStopped = true;

                    if (animator.GetComponentInChildren<Controller_Weapon>())
                        animator.GetComponentInChildren<Controller_Weapon>().BaseAttack();
                }
            }
            else if (Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) > c_ai.attackingDistance)
            {
                agent.isStopped = false;
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

    private void LookAt(GameObject target)
    {
        Vector3 direction = (target.transform.position - agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        agent.transform.rotation = Quaternion.Slerp(agent.transform.transform.rotation, lookRotation, (agent.angularSpeed) * Time.deltaTime);
    }

    // ignoreLayer is missing from this. it will not work propper until fixed
    private bool ForwardRay(string _tag)
    {
        if (Physics.Raycast(agent.transform.position, agent.transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag(_tag))
            {
                return true;
            }
            else
                return false;
        }
        else
        {
            return false;
        }
    }
}
