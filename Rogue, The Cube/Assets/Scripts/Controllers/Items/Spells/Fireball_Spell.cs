using UnityEngine;

public class Fireball_Spell : Controller_Spell
{
    public override void Start()
    {
        base.Start();

        speed = 150;
        rb.AddForce(transform.forward * speed, ForceMode.Force);
        Destroy(gameObject, 5f);
    }
}