using UnityEngine;

public class Controller_Sword : Controller_Weapon
{
    private Animator animator;

    public override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    public override void BaseAttack()
    {
        base.BaseAttack();

        animator.SetTrigger(PickAttack());
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }

    /// <summary>
    /// holds all the attack animations for the sword. if new animations are added, just make the array bigger and and the new trigger to it.
    /// </summary>
    /// <returns>the random animation to be played</returns>
    private string PickAttack()
    {
        string[] attacks = new string[2];
        attacks[0] = "baseattack";
        attacks[1] = "base2attack";
        int i = Random.Range(0, attacks.Length);
        string _str = attacks[i];
        return _str;
    }
}
