using System.Collections;
using UnityEngine;

public class Controller_Sword : Controller_Weapon
{
    private bool swordParry;

    public override void Start()
    {
        base.Start();
    }

    public override void BaseAttack()
    {
        if (Time.time > cooldown && !swordParry)
        {
            base.BaseAttack();
            //StartCoroutine(AttackRoutine());
            GetComponentInChildren<Collision_Sword>().canDoDamage = true;
            animator.SetTrigger("normalAttack");
            cooldown = Time.time + globalCooldown;
        }
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }

    private IEnumerator AttackRoutine()
    {
        animator.SetTrigger(PickAttack());
        yield return new WaitForSeconds(.3f);
    }

    /// <summary>
    /// holds all the attack animations for the sword. if new animations are added, just make the array bigger and and the new trigger to it.
    /// </summary>
    /// <returns>the random animation to be played</returns>
    private string PickAttack()
    {
        string[] attacks = new string[1];
        //attacks[0] = "baseattack";
        //attacks[1] = "base2attack";
        attacks[0] = "base3attack";
        int i = Random.Range(0, attacks.Length);
        string _str = attacks[i];
        return _str;
    }

    public IEnumerator ParryAttack()
    {
        swordParry = true;
        animator.SetTrigger("parry");
        yield return new WaitForSeconds(2f);
        swordParry = false;
    }
}
