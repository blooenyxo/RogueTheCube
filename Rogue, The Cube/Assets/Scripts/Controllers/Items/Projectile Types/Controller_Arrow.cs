using UnityEngine;

public class Controller_Arrow : Controller_Projectile
{
    private float timer;
    private bool canFall = true;

    public override void Start()
    {
        base.Start();
        // set arrow speed or use the one from the special arrows

        //if (speed == 0)
        //    speed = Mathf.RoundToInt(stats.AGILITY.GetValue() * 2f);

        speed = 50; // testing out the fixed arrow speed 

        // speed cannot be greater then 50
        if (speed > 50)
            speed = 50;

        rb.AddForce(transform.forward * speed, ForceMode.Impulse); // this has to be here, the speed variable in set only one row above
        timer = Time.time + .5f; // timer for acrivating gravity, so that the arrow can fall, but only after a given time
    }

    private void Update()
    {
        if (Time.time > timer && canFall)
        {
            rb.useGravity = true;
            canFall = false;
        }
    }

    /// <summary>
    /// an IDEA to make the arrow behave normal upon touching a wall
    /// currenlty, it just drops to the floor, no matter what it hit
    /// fixed the collision matrix (forgot about that). now it collides with walls and enemies and me. ah, and floors too :D
    /// debuff determines if the arrow is magic or not. so we can reuse the same arrow scrip for all the prefabs
    /// </summary>
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(parentTag))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = true;
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            //transform.SetParent(collision.transform);
            if (collision.gameObject.GetComponent<Stats>() && canDoDamage)
            {
                if (buff)
                {
                    if (collision.gameObject.GetComponent<Controller_Buffs>())
                    {
                        collision.gameObject.GetComponent<Controller_Buffs>().AddBuff(buff);
                    }
                    int dmg = stats.DealMagicDamage();
                    collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                }
                else
                {
                    int dmg = stats.DealDamage();
                    collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                    collision.gameObject.GetComponent<Equipment_Visual>().HitMarker(collision.GetContact(0).point);
                }
            }
            canDoDamage = false;
        }
        GetComponent<BoxCollider>().enabled = false;
    }
}