using UnityEngine;

public class Deathball : Spell
{
    public override bool CastSpell(Transform where, Stats casterStats, string parentTag)
    {
        GameObject _prj = Instantiate(effect, where.transform.position, where.transform.rotation);
        _prj.GetComponent<Controller_Projectile>().stats = casterStats;
        _prj.GetComponent<Controller_Projectile>().parentTag = parentTag;
        return true;
    }
}
