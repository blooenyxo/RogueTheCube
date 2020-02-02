using UnityEngine;

public class HealSelf : Spell
{
    public override bool CastSpell(Transform where, Stats casterStats, string parentTag)
    {
        if (casterStats.CurrentHealth < casterStats.HITPOINTS.GetValue())
        {
            casterStats.Heal(Mathf.CeilToInt((Random.Range(casterStats.MINMAGIC.GetValue(), casterStats.MAXMAGIC.GetValue()) + casterStats.INTELIGENCE.GetValue())));
            return true;
        }
        return false;
    }
}
