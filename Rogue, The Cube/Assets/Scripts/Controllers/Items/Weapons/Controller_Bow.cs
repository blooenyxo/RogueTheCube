using UnityEngine;

public class Controller_Bow : Controller_Weapon
{
    public GameObject baseArrow;
    public GameObject firePoint;

    private float cooldown = 0;

    public override void Start()
    {
        base.Start();
    }

    public override void BaseAttack()
    {
        if (Time.time > cooldown && parentStats.UseStamina(staminaUse))
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
        prj.GetComponent<Collision_Controller>().parentTag = parentTag;
        prj.GetComponent<Collision_Controller>().parentStats = parentStats;
    }
}