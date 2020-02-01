using UnityEngine;
using System.Collections;

public class Controller_Staff : Controller_Weapon
{
    public GameObject projectile;
    public GameObject firePoint;

    public int manaCost;

    private bool attacking;

    public override void Start()
    {
        base.Start();
    }

    public override void BaseAttack()
    {
        base.BaseAttack();
        StartCoroutine(ExecuteSwingAttack());
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
        if (attacking == false)
        {
            if (stats.UseMana(manaCost))
            {
                StartCoroutine(ExecuteMissileAttack());
            }
            else
            {
                StartCoroutine(ExecuteSwingAttack());
            }
        }
    }

    public void StaffProjectile()
    {
        GameObject _prj = Instantiate(projectile, firePoint.transform.position, firePoint.transform.rotation);
        _prj.GetComponent<Controller_Projectile>().stats = stats;
        _prj.GetComponent<Controller_Projectile>().parentTag = transform.parent.tag;
    }

    private IEnumerator ExecuteMissileAttack()
    {
        animator.SetTrigger("baseattack");
        attacking = true;
        yield return new WaitForSeconds(1.5f);
        attacking = false;
    }

    private IEnumerator ExecuteSwingAttack()
    {
        animator.SetTrigger("baseattack_swing");
        attacking = true;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}