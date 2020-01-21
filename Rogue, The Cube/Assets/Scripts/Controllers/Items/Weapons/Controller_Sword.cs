using System.Collections;
using UnityEngine;

public class Controller_Sword : Controller_Weapon
{
    public BoxCollider swordCollider;
    private bool swordParry;

    public override void Start()
    {
        swordCollider = GetComponentInChildren<BoxCollider>();
        swordCollider.enabled = false;
        base.Start();
    }

    public override void BaseAttack()
    {
        if (Time.time > cooldown && !swordParry)
        {
            base.BaseAttack();
            cooldown = Time.time + globalCooldown;
            StartCoroutine(AttackRoutine());
        }
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }

    private IEnumerator AttackRoutine()
    {
        animator.SetTrigger(PickAttack());
        swordCollider.enabled = true;
        yield return new WaitForSeconds(cooldown);
        swordCollider.enabled = false;
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
        swordCollider.enabled = false;
        animator.SetTrigger("reset");
        yield return new WaitForSeconds(5f);
        swordParry = false;
    }
}
