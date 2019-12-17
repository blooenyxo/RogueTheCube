using UnityEngine;

public class Controller_Staff : Controller_Weapon
{
    public GameObject projectile;
    public GameObject firePoint;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void BaseAttack()
    {
        base.BaseAttack();

        animator.SetTrigger("baseattack");
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }

    public void StaffProjectile()
    {
        Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
    }
}
