using UnityEngine;

public class Arrow : Controller_Projectile
{
    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.AGILITY.GetValue() * .5f);
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); // this has to be here, the speed variable in set only one row above
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
            canDoDamage = false;
        }
    }
}