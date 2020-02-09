using UnityEngine;

public class Controller_Shield : Controller_Offhand
{
    private Animator animator;
    public bool shieldIsUp;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        shieldIsUp = false;
    }

    public override void UseOffhand()
    {
        animator.SetBool("useShield", true);
        shieldIsUp = true;
    }

    public override void ReleaseOffhand()
    {
        animator.SetBool("useShield", false);
        shieldIsUp = false;
    }
}