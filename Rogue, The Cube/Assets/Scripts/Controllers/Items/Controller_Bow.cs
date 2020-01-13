using UnityEngine;

public class Controller_Bow : Controller_Weapon
{
    public GameObject arrow;
    public GameObject firePoint;

    public float globalCooldown;
    private float cooldown;

    public override void Start()
    {
        base.Start();
    }

    public override void BaseAttack()
    {
        if (Time.time > cooldown)
        {
            base.BaseAttack();
            GameObject _prj = Instantiate(arrow, firePoint.transform.position, firePoint.transform.rotation);
            //_prj.transform.SetParent(this.transform); // projectiles will move and rotate along with the caster. doese not work
            _prj.GetComponent<Controller_Projectile>().stats = stats;

            cooldown = Time.time + globalCooldown;
        }
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }
}
