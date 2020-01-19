using UnityEngine;

public class Controller_Bow : Controller_Weapon
{
    public GameObject arrow;
    public GameObject firePoint;

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

            Controller_Projectile c_p = _prj.GetComponent<Controller_Projectile>();
            c_p.stats = stats;
            c_p.parentTag = transform.parent.tag;

            cooldown = Time.time + globalCooldown;
        }
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
    }
}
