using UnityEngine;

public class Deathbal : Spell
{
    public override void CastSpell(Transform where, Stats casterStats, string parentTag)
    {
        //base.CastSpell(where, casterStats, parentTag);
        GameObject _prj = Instantiate(effect, where.transform.position, where.transform.rotation);
        _prj.GetComponent<Controller_Projectile>().stats = casterStats;
        _prj.GetComponent<Controller_Projectile>().parentTag = parentTag;
    }
}
