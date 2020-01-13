using UnityEngine;

public class Controller_Staff : Controller_Weapon
{
    public GameObject projectile;
    public GameObject firePoint;

    Animator animator;

    public override void Start()
    {
        base.Start();
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
        GameObject _prj = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
        //_prj.transform.SetParent(this.transform); // projectiles will move and rotate along with the caster. doese not work
        _prj.GetComponent<Controller_Projectile>().stats = stats;
    }
}