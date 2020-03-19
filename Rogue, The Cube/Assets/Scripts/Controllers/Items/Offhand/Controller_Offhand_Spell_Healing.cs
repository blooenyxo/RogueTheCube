using UnityEngine;

public class Controller_Offhand_Spell_Healing : Controller_Offhand_Spell
{
    public override void CastSpell()
    {
        base.CastSpell();

        stats.Heal(Mathf.CeilToInt((UnityEngine.Random.Range(stats.MINMAGIC.GetValue(), stats.MAXMAGIC.GetValue()) + stats.INTELIGENCE.GetValue())));
        if (spell.buff != null)
            stats.gameObject.GetComponent<Controller_Buffs>().AddBuff(spell.buff);
    }
}