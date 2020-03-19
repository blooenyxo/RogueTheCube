using UnityEngine;

public class Controller_Offhand_Spell : Controller_Offhand
{
    [HideInInspector] public Spell spell;
    [HideInInspector] public float interval;
    [HideInInspector] public float nextTime;

    public override void Start()
    {
        base.Start();

        spell = GetComponentInParent<Controller_Equipment>().currentEquipment[3].spell;
        interval = spell.castInterval;
    }

    public override void UseOffhand()
    {
        base.UseOffhand();

        if (Time.time > nextTime)
        {
            if (stats.UseMana(spell.manaCost))
            {
                CastSpell();
                nextTime = Time.time + GameManager.globalCooldown;
            }
        }
    }

    public virtual void CastSpell() { }
}