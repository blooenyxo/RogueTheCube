using UnityEngine;
using System.Collections;

public class Controller_Staff : Controller_Weapon
{
    public Transform firePoint;
    private bool attacking;

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

    //public override void SpecialAttack()
    //{
    //base.SpecialAttack();
    //if (attacking == false)
    //{
    //    if (equipment.currentEquipment[3] != null)
    //    {
    //        if (equipment.currentEquipment[3].ITEM_TYPE == ITEMTYPE.SPELL)
    //            if (stats.CurrentMana >= equipment.currentEquipment[3].spell.manaCost)
    //                StartCoroutine(ExecuteSpellAttack());
    //    }
    //}
    //}

    //public void StaffSpell()
    //{
    //    if (stats.UseMana(equipment.currentEquipment[3].spell.manaCost) && equipment.currentEquipment[3].ITEM_TYPE == ITEMTYPE.SPELL)
    //    {            
    //        //stats.Heal(Mathf.CeilToInt((Random.Range(stats.MINMAGIC.GetValue(), stats.MAXMAGIC.GetValue()) + stats.INTELIGENCE.GetValue())));
    //        Get
    //    }
    //    else
    //    {
    //        // message "Not enogh Mana"
    //    }
    //}

    //private IEnumerator ExecuteSpellAttack()
    //{
    //    animator.SetTrigger(equipment.currentEquipment[3].spell.title);
    //    attacking = true;
    //    yield return new WaitForSeconds(1.5f);
    //    attacking = false;
    //}

    private IEnumerator ExecuteSwingAttack()
    {
        animator.SetTrigger("baseattack_swing");
        attacking = true;
        yield return new WaitForSeconds(1f);
        attacking = false;
    }
}