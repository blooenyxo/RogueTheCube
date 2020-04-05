using System.Collections;
using UnityEngine;

public class Controller_Staff : Controller_Weapon
{
    private bool attacking;

    public override void Start()
    {
        base.Start();

        GetComponentInChildren<Collision_Controller>().parentStats = parentStats;
        GetComponentInChildren<Collision_Controller>().parentTag = parentTag;
    }

    public override void BaseAttack()
    {
        base.BaseAttack();
        if (attacking == false)
            StartCoroutine(ExecuteSwingAttack());
    }

    private IEnumerator ExecuteSwingAttack()
    {
        animator.SetTrigger("baseattack_swing");
        attacking = true;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}