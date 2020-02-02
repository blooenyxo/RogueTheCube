﻿using UnityEngine;
using System.Collections;

public class Controller_Staff : Controller_Weapon
{
    public Transform firePoint;
    private bool attacking;
    private Spell spell;

    public override void Start()
    {
        base.Start();
        spell = GetComponent<Spell>();
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
            if (stats.CurrentMana >= spell.manaCost)
                StartCoroutine(ExecuteSpellAttack());
        }
    }

    public void StaffSpell()
    {
        //Debug.Log("fire");
        if (spell.CastSpell(firePoint, stats, transform.parent.tag))
            stats.UseMana(spell.manaCost);
    }

    private IEnumerator ExecuteSpellAttack()
    {
        animator.SetTrigger(spell.title);
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