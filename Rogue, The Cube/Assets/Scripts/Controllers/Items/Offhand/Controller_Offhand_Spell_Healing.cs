using UnityEngine;

public class Controller_Offhand_Spell_Healing : Controller_Offhand_Spell
{
    public override void CastSpell()
    {
        base.CastSpell();

        parentStats.Heal(Mathf.CeilToInt((UnityEngine.Random.Range(parentStats.MINMAGIC.GetValue(), parentStats.MAXMAGIC.GetValue()) + parentStats.INTELIGENCE.GetValue())));
        if (spell.buff != null)
            parentStats.gameObject.GetComponent<Controller_Buffs>().AddBuff(spell.buff);
    }
}