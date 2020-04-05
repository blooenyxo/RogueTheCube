using UnityEngine;

public class Deathball_Spell : Controller_Spell
{
    public override void Start()
    {
        base.Start();

        speed = 50;
        rb.AddForce(transform.forward * speed, ForceMode.Force);
        Destroy(gameObject, 5f);
    }
}