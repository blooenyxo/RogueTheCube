using UnityEngine;

/// <summary>
/// the basic thing here is that we itterate the index of the attack array. if you dont attack for more then 1 second, the index goes back to 0;
/// simple, easy. the collision in handled in the child (Collision_Sword)
/// TODO: move player forward when attacking - done! but needs some adjustments
/// </summary>
public class Controller_Sword : Controller_Weapon
{
    private int currentAttackIndex = 0;
    private float resetAttackOrderTime = 0;
    private string[] attacks;
    private float cooldown = 0;

    public override void Start()
    {
        base.Start();

        // holds all the attack animations for the sword. if new animations are added, just make the array bigger and and the new trigger to it.
        attacks = new string[3];
        attacks[0] = "attack_0";
        attacks[1] = "attack_1";
        attacks[2] = "attack_2";

        GetComponentInChildren<Collision_Controller>().parentStats = parentStats;
        GetComponentInChildren<Collision_Controller>().parentTag = parentTag;
    }

    private void Update()
    {
        if (Time.time > resetAttackOrderTime)
        {
            currentAttackIndex = 0;
        }
    }

    public override void BaseAttack()
    {
        if (Time.time > cooldown && parentStats.UseStamina(staminaUse))
        {
            // dynamic cooldown, based on what stage of the sword routine you are in :)
            if (currentAttackIndex == 2)
            {
                //parentRigidbody.AddForce(50 * (transform.forward + transform.up), ForceMode.Impulse);
                GetComponentInChildren<Collision_Controller>().damageModifier = 5;
                cooldown = Time.time + 1f;
            }
            else
            {
                GetComponentInChildren<Collision_Controller>().damageModifier = 1;
                cooldown = Time.time + .2f;
            }

            animator.SetTrigger(PickAttack());
            resetAttackOrderTime = Time.time + 1f;
        }
    }

    private string PickAttack()
    {
        string _str = attacks[currentAttackIndex];
        currentAttackIndex++;

        if (currentAttackIndex >= attacks.Length)
        {
            currentAttackIndex = 0;
        }

        return _str;
    }
}