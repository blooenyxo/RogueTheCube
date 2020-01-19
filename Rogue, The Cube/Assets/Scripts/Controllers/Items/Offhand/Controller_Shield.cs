using UnityEngine;

public class Controller_Shield : Controller_Offhand
{
    private Animator animator;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void UseOffhand()
    {
        shieldCollider.enabled = true;
        animator.SetBool("useShield", true);
    }

    public override void ReleaseOffhand()
    {
        shieldCollider.enabled = false;
        animator.SetBool("useShield", false);
    }
}