using UnityEngine;

/// <summary>
/// keep this script simple. if we need DOT damage, maybe consider making a secondary method, OnTriggerStay() for that.
/// </summary>
public class Deathball_Spell : Controller_Spell
{
    public override void Start()
    {
        base.Start();
        rb.AddForce(transform.forward * speed, ForceMode.Force);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() && !other.gameObject.CompareTag(parentTag))
        {
            int dmg = casterStats.DealMagicDamage();
            other.gameObject.GetComponent<Stats>().TakeDamage(dmg);
            other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.gameObject.transform.position);

            // apply buff
            if (spell.buff != null)
                other.gameObject.GetComponent<Controller_Buffs>().AddBuff(spell.buff);
        }
        else if (other.gameObject.CompareTag("Environment") && destroyOnWallhit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (damageOverTime)
        {
            if (Time.time > nextTime)
            {
                if (other.gameObject.GetComponent<Stats>() && !other.gameObject.CompareTag(parentTag))
                {
                    int dmg = casterStats.DealMagicDamage();
                    other.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                    other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.gameObject.transform.position);
                    nextTime = Time.time + interval;
                }
            }
        }
    }
}