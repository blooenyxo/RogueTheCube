using UnityEngine;

public class StaffProjectile : Controller_Projectile
{
    public GameObject sphereToAnimate;

    private bool up = true;
    private float cooldown = 0f;
    private float globalCooldown = 1f;
    private Vector3 pulseSize = new Vector3(4f, 4f, 4f);

    private readonly float interval = .5f;
    private float nextTime = 0f;

    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.INTELIGENCE.GetValue() * 10f);
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
                    int dmg = stats.DealDamage();
                    other.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                    //other.gameObject.GetComponent<Equipment_Visual>().HitMarker(other.transform.GetContact(0).point);
                }
            }
            nextTime += interval;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void Update()
    {
        Pulsate();
    }

    void Pulsate()
    {
        if (Time.time > cooldown)
        {
            up = !up;
            cooldown = Time.time + globalCooldown;
        }

        if (up)
            sphereToAnimate.transform.localScale += pulseSize * Time.deltaTime;
        else if (!up)
            sphereToAnimate.transform.localScale -= pulseSize * Time.deltaTime;
    }
}