using UnityEngine;

public class Collision_Controller : MonoBehaviour
{
    public Stats parentStats;
    public string parentTag;
    public Buff buff;
    public int damageModifier = 1;

    /// <summary>
    /// collision uses collision matrix again, like with arrows. works great for now
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(parentTag)) // check if i don`t hit myself
        {
            int dmg = parentStats.DealDamage();
            dmg *= damageModifier;

            //Debug.Log(" doing damage ");

            if (other.gameObject.GetComponentInParent<Stats>())
            {
                other.gameObject.GetComponentInParent<Stats>().TakeDamage(dmg);

                if (buff) // add buff if exists
                {
                    if (other.gameObject.GetComponentInParent<Controller_Buffs>())
                    {
                        other.gameObject.GetComponentInParent<Controller_Buffs>().AddBuff(buff);
                    }
                }

                if (other.gameObject.GetComponentInParent<Controller_AI>()) // alert only enemies
                {
                    other.gameObject.GetComponentInParent<Controller_AI>().AlertNearby(parentTag);
                    other.gameObject.GetComponentInParent<Controller_AI>().animator.SetTrigger("isTakingDamage");
                }

                if (other.gameObject.GetComponent<Controller_Character_Body_Invulnerability>())
                    other.gameObject.GetComponent<Controller_Character_Body_Invulnerability>().Invincible();

                if (other.gameObject.GetComponent<Controller_Character_Body_Knockback>())
                    other.gameObject.GetComponent<Controller_Character_Body_Knockback>().Knockback(parentStats.gameObject.transform); // just because we allready have this reference. might as well.
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