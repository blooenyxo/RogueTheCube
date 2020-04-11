using UnityEngine;

public class TakingDamage : StateMachineBehaviour
{
    private Controller_AI c_ai;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        c_ai = animator.GetComponent<Controller_AI>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        c_ai.isEngaged = false;
    }
}
