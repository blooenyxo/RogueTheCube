using UnityEngine;

/// <summary>
/// keep this script simple. if we need DOT damage, maybe consider making a secondary method, OnTriggerStay() for that.
/// </summary>
public class Controller_StaffProjectile : Controller_Projectile
{
    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.INTELIGENCE.GetValue());
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Stats>() && !other.gameObject.CompareTag(parentTag))
        {
            int dmg = stats.DealMagicDamage();
            other.gameObject.GetComponent<Stats>().TakeDamage(dmg);
            other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.gameObject.transform.position);

            // apply buff
            if (buff != null)
                other.gameObject.GetComponent<Controller_Buffs>().AddBuff(buff);
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
                    int dmg = stats.DealMagicDamage();
                    other.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                    other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.gameObject.transform.position);
                    nextTime = Time.time + interval;
                }
            }
        }
    }
}