using UnityEngine;

public class Controller_Character_Body_Collision : MonoBehaviour
{
    public Stats parentStats;
    public string parentTag;
    public int touchingDamage;

    private void OnTriggerEnter(Collider other)
    {
        Damage(other);
    }

    private void OnTriggerStay(Collider other)
    {
        Damage(other);
    }

    void Damage(Collider other)
    {
        if (!other.gameObject.CompareTag(parentTag) && !other.gameObject.CompareTag("Weapon")) // check if i don`t hit myself
        {
            if (other.gameObject.GetComponentInParent<Stats>())
            {
                other.gameObject.GetComponentInParent<Stats>().TakeDamage(touchingDamage);
            }
            if (other.gameObject.GetComponent<Controller_Character_Body_Invulnerability>())
                other.gameObject.GetComponent<Controller_Character_Body_Invulnerability>().Invincible();

            if (other.gameObject.GetComponent<Controller_Character_Body_Knockback>())
                other.gameObject.GetComponent<Controller_Character_Body_Knockback>().Knockback(parentStats.gameObject.transform); // just because we allready have this reference. might as well.
        }
    }
}