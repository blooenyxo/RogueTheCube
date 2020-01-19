using UnityEngine;

public class Controller_Staff : Controller_Weapon
{
    public GameObject projectile;
    public GameObject firePoint;

    public override void Start()
    {
        base.Start();
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
        _prj.GetComponent<Controller_Projectile>().stats = stats;
    }
}