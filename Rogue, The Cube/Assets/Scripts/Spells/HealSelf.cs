using UnityEngine;

public class HealSelf : Spell
{
    public override void CastSpell(Transform where, Stats casterStats, string parentTag)
    {
        //base.CastSpell(where, casterStats, parentTag);
        casterStats.Heal(Mathf.CeilToInt((Random.Range(casterStats.MINMAGIC.GetValue(), casterStats.MAXMAGIC.GetValue()) + casterStats.INTELIGENCE.GetValue())));
    }
}
