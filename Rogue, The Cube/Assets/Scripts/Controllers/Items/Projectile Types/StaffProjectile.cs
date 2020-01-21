using UnityEngine;

public class StaffProjectile : Controller_Projectile
{
    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.INTELIGENCE.GetValue() * 1f);
        rb.AddForce(transform.forward * speed);
    }
}