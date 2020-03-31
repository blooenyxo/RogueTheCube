using UnityEngine;

public class Collision_Sword : MonoBehaviour
{
    /// <summary>
    /// collision uses collision matrix again, like with arrows. works great for now
    /// </summary>
    //private void OnCollisionEnter(Collision collision) { }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(GetComponentInParent<Controller_Sword>().parentTag) && !other.gameObject.CompareTag("Shield")) // check if i don`t hit myself
        {
            int dmg = GetComponentInParent<Controller_Weapon>().stats.DealDamage();
            if (other.gameObject.GetComponentInParent<Stats>())
            {
                other.gameObject.GetComponentInParent<Stats>().TakeDamage(dmg);

                if (other.gameObject.GetComponent<Controller_Character_Body>())
                    other.gameObject.GetComponent<Controller_Character_Body>().KnockBack(this.transform.parent.parent.forward);
            }
        }
        else if (other.gameObject.CompareTag("Shield"))
        {
            if (other.gameObject.GetComponentInParent<Controller_Shield>().shieldIsUp)
            {
                // parry
            }
        }
    }
}
//if (!collision.gameObject.CompareTag(GetComponentInParent<Controller_Weapon>().parentTag))
//{
//    if (collision.gameObject.GetComponentInParent<Controller_Shield>())
//    {
//        if (collision.gameObject.GetComponentInParent<Controller_Shield>().shieldIsUp == false)
//        {
//            int dmg = GetComponentInParent<Controller_Weapon>().stats.DealDamage();
//            if (collision.gameObject.GetComponent<Stats>())
//            {
//                //Debug.Log(collision.gameObject.tag + " took " + dmg + " damage from " + parentTag);
//                //Debug.Log("shield is not up, doing damage");
//                collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
//                //other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.GetContact(0).point);
//            }
//        }
//        else if (collision.gameObject.GetComponentInParent<Controller_Shield>().shieldIsUp == true)
//        {
//            if (collision.gameObject.GetComponentInParent<Controller_Shield>().parentTag == GetComponentInParent<Controller_Weapon>().parentTag)
//            {
//                //Debug.Log("hit my shield");
//                return;
//            }

//            StartCoroutine(GetComponentInParent<Controller_Sword>().ParryAttack());
//            // Enemy parry animation EXTRAS go here !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

//            //Debug.Log("Parry");
//            canDoDamage = false;
//            return;
//        }
//    }
//    else if (canDoDamage && !collision.gameObject.GetComponentInParent<Controller_Shield>())
//    {
//        int dmg = GetComponentInParent<Controller_Weapon>().stats.DealDamage();
//        if (collision.gameObject.GetComponent<Stats>())
//        {
//            //Debug.Log(collision.gameObject.tag + " took " + dmg + " damage from " + parentTag);
//            //Debug.Log("no shield present, doing damage");
//            collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
//            //other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.GetContact(0).point);
//        }
//    }
//}