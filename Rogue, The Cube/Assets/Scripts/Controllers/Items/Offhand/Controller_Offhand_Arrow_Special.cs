using UnityEngine;

public class Controller_Offhand_Arrow_Special : Controller_Offhand
{
    private Arrow arrow;
    private float cooldown;
    private Controller_Equipment equipment;

    public GameObject firePoint;

    public override void Start()
    {
        base.Start();

        equipment = GetComponentInParent<Controller_Equipment>();
        arrow = GetComponentInParent<Controller_Equipment>().currentEquipment[3].arrow;
    }

    public override void UseOffhand()
    {
        base.UseOffhand();

        CastSpecialArrow();
    }

    public override void ReleaseOffhand()
    {
        base.ReleaseOffhand();
    }

    private void CastSpecialArrow()
    {
        if (Time.time > cooldown)
        {
            if (equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks > 0)
            {
                SetupSpecialArrow(arrow.visualModel);
                equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks--;
                equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().AdjustStackText();
                cooldown = Time.time + GameManager.globalCooldown;

                if (equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks <= 0)
                {
                    Destroy(equipment.currentEquipmentGameObjects[3]);
                    equipment.Unequip(3);
                }
            }

        }
    }

    private void SetupSpecialArrow(GameObject visualModel)
    {
        GameObject prj = Instantiate(visualModel, firePoint.transform.position, firePoint.transform.rotation);
        Controller_Projectile c_p = prj.GetComponent<Controller_Projectile>();
        c_p.stats = stats;
        c_p.parentTag = transform.parent.tag;
        c_p.buff = arrow.debuff;

        GameObject pe = Instantiate(arrow.particleEffect, firePoint.transform.position, firePoint.transform.rotation);
        pe.transform.SetParent(prj.transform);
    }
}