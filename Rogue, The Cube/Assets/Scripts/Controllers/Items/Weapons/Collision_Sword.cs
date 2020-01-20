﻿using UnityEngine;

public class Collision_Sword : Controller_Sword
{
    /// <summary>
    /// collision uses collision matrix again, like with arrows. works great for now
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // this first part is for hitting you own shield. If left unchecked, you can accidentally parry yourself with your shield
        if (collision.gameObject.GetComponentInParent<Controller_Shield>())
        {
            if (collision.gameObject.GetComponentInParent<Controller_Shield>().parentTag == parentTag)
            {
                //Debug.Log("hit my shield");
                return;
            }
        }
        // the second part is ment to see if you hit something else. first a shield, and then a character
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