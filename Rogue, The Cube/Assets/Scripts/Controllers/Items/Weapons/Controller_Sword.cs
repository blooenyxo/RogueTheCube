using UnityEngine;

/// <summary>
/// the basic thing here is that we itterate the index of the attack array. if you dont attack for more then 1 second, the index goes back to 0;
/// simole, easy. the collision in handled in the child (Collision_Sword)
/// TODO: move player forward when attacking
/// </summary>
public class Controller_Sword : Controller_Weapon
{
    private int currentAttackIndex = 0;
    private float resetAttackOrderTime = 0;
    private string[] attacks;
    private Rigidbody parentRb;

    public override void Start()
    {
        base.Start();

        parentRb = GetComponentInParent<Rigidbody>();

        attacks = new string[3];
        attacks[0] = "attack_0";
        attacks[1] = "attack_1";
        attacks[2] = "attack_2";
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
        if (Time.time > cooldown && stats.UseStamina(staminaUse))
        {
            if (currentAttackIndex == 2)
                parentRb.AddForce(2 * (transform.forward  + transform.up), ForceMode.Impulse);

            base.BaseAttack();
            animator.SetTrigger(PickAttack());

            cooldown = Time.time + GameManager.globalCooldown;
            resetAttackOrderTime = Time.time + 1f;
        }
    }

    /// <summary>
    /// holds all the attack animations for the sword. if new animations are added, just make the array bigger and and the new trigger to it.
    /// </summary>
    /// <returns>the random animation to be played</returns>
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