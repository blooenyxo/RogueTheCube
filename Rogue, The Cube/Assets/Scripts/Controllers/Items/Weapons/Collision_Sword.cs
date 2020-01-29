using UnityEngine;

public class Collision_Sword : Controller_Sword
{
    readonly int interval = 1;
    int nextTime = 0;
    /// <summary>
    /// collision uses collision matrix again, like with arrows. works great for now
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= nextTime)
        {
            // this first part is for hitting you own shield. If left unchecked, you can accidentally parry yourself with your shield
            if (collision.gameObject.GetComponentInParent<Controller_Shield>())
            {
                if (collision.gameObject.GetComponentInParent<Controller_Shield>().parentTag == parentTag)
                {
                    //Debug.Log("hit my shield");
                    return;
                }
                else // here we act if the shield is not mine
                {
                    StartCoroutine(ParryAttack());
                    if (GetComponentInParent<AI_Routine>())
                    {
                        GetComponentInParent<AI_Routine>().Stop();
                    }
                }
            }
            else if (!collision.gameObject.CompareTag(parentTag))
            {
                int dmg = stats.DealDamage();
                if (collision.gameObject.GetComponent<Stats>())
                {
                    Debug.Log(collision.gameObject.tag + " took " + dmg + " damage from " + parentTag);
                    collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                    collision.gameObject.GetComponent<Equipment_Visual>().HitMarker(collision.GetContact(0).point);
                }
            }
            nextTime += interval;
        }
    }
}