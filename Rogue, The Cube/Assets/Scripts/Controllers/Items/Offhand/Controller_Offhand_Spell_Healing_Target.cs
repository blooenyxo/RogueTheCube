using UnityEngine;

public class Controller_Offhand_Spell_Healing_Target : Controller_Offhand_Spell
{
    public override void CastSpell()
    {
        base.CastSpell();

        target.GetComponentInParent<Stats>().Heal(Mathf.CeilToInt((Random.Range(parentStats.MINMAGIC.GetValue(), parentStats.MAXMAGIC.GetValue()) + parentStats.INTELIGENCE.GetValue())));
        if (spell.buff != null)
            target.GetComponentInParent<Controller_Buffs>().AddBuff(spell.buff);

    }
}