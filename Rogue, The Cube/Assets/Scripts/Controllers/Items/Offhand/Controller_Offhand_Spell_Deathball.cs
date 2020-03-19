using UnityEngine;

public class Controller_Offhand_Spell_Deathball : Controller_Offhand_Spell
{
    public Transform firePoint;
    [HideInInspector] public GameObject visualModel;

    public override void Start()
    {
        base.Start();

        visualModel = spell.visualModel;
    }

    public override void CastSpell()
    {
        base.CastSpell();

        if (Time.time > nextTime)
        {
            GameObject _spell = Instantiate(visualModel, firePoint.transform.position, firePoint.transform.rotation);
            Controller_Spell spell_cs = _spell.GetComponent<Controller_Spell>();

            spell_cs.spell = spell;
            spell_cs.casterStats = stats;
            spell_cs.parentTag = parentTag;

            nextTime = Time.time + interval;
        }
    }
}