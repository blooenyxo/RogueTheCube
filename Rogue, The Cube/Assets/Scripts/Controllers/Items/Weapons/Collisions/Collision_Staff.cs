using UnityEngine;

public class Collision_Staff : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(GetComponentInParent<Controller_Weapon>().parentTag))
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