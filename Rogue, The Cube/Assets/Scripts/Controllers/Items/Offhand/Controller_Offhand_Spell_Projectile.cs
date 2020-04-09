using UnityEngine;

public class Controller_Offhand_Spell_Projectile : Controller_Offhand_Spell
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

            _spell.GetComponent<Controller_Spell>().spell = spell;
            _spell.GetComponent<Controller_Spell>().casterStats = parentStats;
            _spell.GetComponent<Controller_Spell>().parentTag = parentTag;

            //_spell.GetComponentInChildren<Collision_Controller>().parentTag = parentTag;
            _spell.GetComponentInChildren<Collision_Controller>().parentStats = parentStats;
            _spell.GetComponentInChildren<Collision_Controller>().buff = spell.buff;

            nextTime = Time.time + interval;
        }
    }
}