using UnityEngine;

public class StaffProjectile : Controller_Projectile
{
    public GameObject sphereToAnimate;

    private readonly float interval = .1f;
    private float nextTime = 0f;

    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.INTELIGENCE.GetValue());
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time >= nextTime)
        {
            if (!other.gameObject.CompareTag(parentTag))
            {
                if (other.gameObject.GetComponent<Stats>())
                {
                    int dmg = stats.DealMagicDamage();
                    other.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                    other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.gameObject.transform.position);
                }
            }
            nextTime = Time.time + interval;
        }
    }
}