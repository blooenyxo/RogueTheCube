using UnityEngine;

public class Controller_Shield : Controller_Offhand
{
    private Animator animator;
    private BoxCollider shieldCollider;

    public override void Start()
    {
        base.Start();
        shieldCollider = GetComponentInChildren<BoxCollider>();
        shieldCollider.enabled = false;
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