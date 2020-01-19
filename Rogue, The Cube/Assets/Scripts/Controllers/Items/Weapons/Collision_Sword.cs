using UnityEngine;

public class Collision_Sword : Controller_Sword
{
    /// <summary>
    /// collision uses collision matrix again, like with arrows. works great for now
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            //Debug.Log("shield block");
            StartCoroutine(ParryAttack());
        }
        else if (!collision.gameObject.CompareTag(parentTag))
        {
            int dmg = stats.DealDamage();
            if (collision.gameObject.GetComponent<Stats>())
            {
                //Debug.Log("took " + dmg + " damage");
                collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                collision.gameObject.GetComponent<Equipment_Visual>().HitMarker(collision.GetContact(0).point);
            }
        }
    }
}