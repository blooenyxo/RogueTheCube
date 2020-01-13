using UnityEngine;

public class StaffProjectile : Controller_Projectile
{
    public override void Start()
    {
        base.Start();
        speed = stats.INTELIGENCE.GetValue();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}