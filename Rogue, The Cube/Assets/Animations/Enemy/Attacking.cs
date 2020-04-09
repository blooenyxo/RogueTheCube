using UnityEngine;

public class Attacking : StateMachineBehaviour
{
    private Controller_AI c_ai;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        c_ai = animator.GetComponent<Controller_AI>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (c_ai.mainTarget != null)
        {
            Vector3 direction = (c_ai.mainTarget.transform.position - animator.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            animator.transform.rotation = Quaternion.Slerp(animator.transform.transform.rotation, lookRotation, 1f * Time.deltaTime);


            if (Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) > c_ai.attackingDistance)
            {
                int moveSpeed = animator.GetComponent<Stats_Enemy>().MOVESPEED.GetValue();
                if (moveSpeed < 0)
                    moveSpeed = 0;

                animator.transform.position = Vector3.MoveTowards(animator.transform.position, c_ai.mainTarget.transform.position, moveSpeed * Time.deltaTime);
            }
            else if (Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) <= c_ai.attackingDistance && Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) > 1f)
            {
                if (ForwardRay(animator.gameObject, "Player"))
                {
                    if (animator.GetComponentInChildren<Controller_Weapon>())
                        animator.GetComponentInChildren<Controller_Weapon>().BaseAttack();
                }
            }
            else if (Vector3.Distance(animator.transform.position, c_ai.mainTarget.transform.position) <= 1f)
            {
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, c_ai.mainTarget.transform.position, -animator.GetComponent<Stats_Enemy>().MOVESPEED.GetValue() * Time.deltaTime);
            }

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // ignoreLayer is missing from this. it will not work propper until fixed
    private bool ForwardRay(GameObject me, string _tag)
    {
        if (Physics.Raycast(me.transform.position, me.transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
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
