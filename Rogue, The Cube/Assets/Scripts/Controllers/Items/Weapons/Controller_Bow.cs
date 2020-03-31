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
        if (Time.time > cooldown && stats.UseStamina(staminaUse))
        {
            base.BaseAttack();
            CreateArrow(baseArrow);
            cooldown = Time.time + GameManager.globalCooldown;
        }
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();

    }

    private void CreateArrow(GameObject visualModel)
    {
        GameObject prj = Instantiate(visualModel, firePoint.transform.position, firePoint.transform.rotation);
        Controller_Projectile c_p = prj.GetComponent<Controller_Projectile>();
        c_p.stats = stats;
        c_p.parentTag = transform.parent.parent.tag;  // TODO : maybe think about a better way to fix this. seems loose...
    }
}