using UnityEngine;

public class Collision_Sword : MonoBehaviour
{
    /// <summary>
    /// collision uses collision matrix again, like with arrows. works great for now
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // this first part is for hitting you own shield. If left unchecked, you can accidentally parry yourself with your shield
        if (collision.gameObject.GetComponentInParent<Controller_Shield>())
        {
            if (collision.gameObject.GetComponentInParent<Controller_Shield>().parentTag == GetComponentInParent<Controller_Weapon>().parentTag)
            {
                //Debug.Log("hit my shield");
                return;
            }
            else // here we act if the shield is not mine
            {
                StartCoroutine(GetComponentInParent<Controller_Sword>().ParryAttack());
                if (GetComponentInParent<AI_Routine>())
                {
                    GetComponentInParent<AI_Routine>().Stop();
                }
            }
        }
        else if (!collision.gameObject.CompareTag(GetComponentInParent<Controller_Weapon>().parentTag))
        {
            int dmg = GetComponentInParent<Controller_Weapon>().stats.DealDamage();
            if (collision.gameObject.GetComponent<Stats>())
            {
                //Debug.Log(collision.gameObject.tag + " took " + dmg + " damage from " + parentTag);
                collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                collision.gameObject.GetComponent<Equipment_Visual>().HitMarker(collision.GetContact(0).point);
            }
        }
    }
}