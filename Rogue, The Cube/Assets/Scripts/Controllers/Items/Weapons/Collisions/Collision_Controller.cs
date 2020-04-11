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

            if (other.gameObject.GetComponentInParent<Stats>())
            {
                other.gameObject.GetComponentInParent<Stats>().TakeDamage(dmg);
            }

            if (buff) // add buff if exists
                if (other.gameObject.GetComponentInParent<Controller_Buffs>())
                    other.gameObject.GetComponentInParent<Controller_Buffs>().AddBuff(buff);

            if (other.gameObject.GetComponentInParent<Controller_AI>())
                other.gameObject.GetComponentInParent<Controller_AI>().AlertNearby(parentTag);  // alert only enemies

            if (other.gameObject.GetComponent<Controller_Character_Body_Invulnerability>())
                other.gameObject.GetComponent<Controller_Character_Body_Invulnerability>().Invincible();

            int rnd = Random.Range(0, 100);
            if (rnd >= 25) // 3 out of 4 times this will trigger
            {
                if (other.gameObject.GetComponentInParent<Rigidbody>())
                {
                    other.gameObject.GetComponentInParent<Rigidbody>().AddForce(transform.up * 2f, ForceMode.Impulse);
                    other.gameObject.GetComponentInParent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);
                }
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