using UnityEngine;

public class Arrow : Controller_Projectile
{
    private float timer;
    private bool canFall = true;
    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.AGILITY.GetValue());
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); // this has to be here, the speed variable in set only one row above
        timer = Time.time + .5f;
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
    /// </summary>
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(parentTag))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.useGravity = true;

            if (collision.gameObject.GetComponent<Stats>() && canDoDamage)
            {
                int dmg = stats.DealDamage();
                collision.gameObject.GetComponent<Stats>().TakeDamage(dmg);
                collision.gameObject.GetComponent<Equipment_Visual>().HitMarker(collision.GetContact(0).point);
            }

            // disable the arrow after falling to the floor. this is for polish later on
            //if (collision.gameObject.CompareTag("Floor"))
            //{
            //    GetComponent<BoxCollider>().enabled = false;
            //    rb.useGravity = false;
            //}
            canDoDamage = false;
        }
    }
}