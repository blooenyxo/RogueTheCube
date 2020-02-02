using UnityEngine;

public class Controller_Bow : Controller_Weapon
{
    public GameObject baseArrow;
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
            CreateArrow(baseArrow);
            cooldown = Time.time + globalCooldown;
        }
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
        if (Time.time > cooldown)
        {
            if (Equipment.instance.currentEquipment[3] != null)
            {
                CreateArrow(Equipment.instance.currentEquipment[3].VISUAL_MODEL);
            }
            cooldown = Time.time + globalCooldown;
        }
    }

    private void CreateArrow(GameObject arrow)
    {
        GameObject _prj = Instantiate(arrow, firePoint.transform.position, firePoint.transform.rotation);
        Controller_Projectile c_p = _prj.GetComponent<Controller_Projectile>();
        c_p.stats = stats;
        c_p.parentTag = transform.parent.tag;
    }
}
