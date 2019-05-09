using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Bow : Controller_Weapon
{
    public GameObject arrow;
    public GameObject firePoint;
    public override void BaseAttack()
    {
        base.BaseAttack();
        Instantiate(arrow, firePoint.transform.position, firePoint.transform.rotation);
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }
}
