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
            if (equipment.currentEquipment[3] != null)
            {
                if (equipment.currentEquipment[3].ITEM_TYPE == ITEMTYPE.ARROW)
                {
                    if (equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks > 0)
                    {
                        SetupSpecialArrow(equipment.currentEquipment[3].arrow.visualModel);
                        equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks--;
                        equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().AdjustStackText();
                        cooldown = Time.time + globalCooldown;

                        if (equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks <= 0)
                        {
                            Destroy(equipment.currentEquipmentGameObjects[3]);
                            equipment.Unequip(3);
                        }
                    }
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
        c_p.buff = equipment.currentEquipment[3].arrow.debuff;
        c_p.speed = equipment.currentEquipment[3].arrow.speed;

        GameObject pe = Instantiate(equipment.currentEquipment[3].arrow.particleEffect, firePoint.transform.position, firePoint.transform.rotation);
        pe.transform.SetParent(prj.transform);
    }

    private void CreateArrow(GameObject visualModel)
    {
        GameObject prj = Instantiate(visualModel, firePoint.transform.position, firePoint.transform.rotation);
        Controller_Projectile c_p = prj.GetComponent<Controller_Projectile>();
        c_p.stats = stats;
        c_p.parentTag = transform.parent.tag;
    }
}
