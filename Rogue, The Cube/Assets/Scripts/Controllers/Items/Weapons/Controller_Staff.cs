using UnityEngine;
using System.Collections;

public class Controller_Staff : Controller_Weapon
{
    public Transform firePoint;
    private bool attacking;
    private Spell spell;

    public override void Start()
    {
        base.Start();
    }

    public override void BaseAttack()
    {
        base.BaseAttack();
        if (attacking == false)
            StartCoroutine(ExecuteSwingAttack());
    }

    public override void SpecialAttack()
    {
        base.SpecialAttack();
        if (attacking == false)
        {
            if (equipment.currentEquipment[3] != null)
            {
                if (equipment.currentEquipment[3].ITEM_TYPE == ITEMTYPE.SPELL)
                    if (stats.CurrentMana >= equipment.currentEquipment[3].spell.manaCost)
                        StartCoroutine(ExecuteSpellAttack());
            }
        }
    }

    public void StaffSpell()
    {
        if (stats.UseMana(equipment.currentEquipment[3].spell.manaCost))
        {
            if (equipment.currentEquipment[3].ITEM_TYPE == ITEMTYPE.SPELL)
            {
                if (equipment.currentEquipment[3].spell.attacking)
                {
                    GameObject _prj = Instantiate(equipment.currentEquipment[3].spell.visualModel, firePoint.transform.position, firePoint.transform.rotation); ;
                    _prj.GetComponent<Controller_Projectile>().stats = stats;
                    _prj.GetComponent<Controller_Projectile>().parentTag = parentTag;
                    _prj.GetComponent<Controller_Projectile>().destroyOnWallhit = equipment.currentEquipment[3].spell.interactWithWalls;

                    // apply buff
                    if (equipment.currentEquipment[3].spell.buff != null)
                        _prj.GetComponent<Controller_Projectile>().buff = equipment.currentEquipment[3].spell.buff;
                }
                else if (equipment.currentEquipment[3].spell.healing)
                {
                    stats.Heal(Mathf.CeilToInt((Random.Range(stats.MINMAGIC.GetValue(), stats.MAXMAGIC.GetValue()) + stats.INTELIGENCE.GetValue())));
                }
            }
        }
        else
        {
            // message "Not enogh Mana"
        }
    }

    private IEnumerator ExecuteSpellAttack()
    {
        animator.SetTrigger(equipment.currentEquipment[3].spell.title);
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