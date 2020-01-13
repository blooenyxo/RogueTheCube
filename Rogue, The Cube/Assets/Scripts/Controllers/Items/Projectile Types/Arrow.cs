using UnityEngine;

public class Arrow : Controller_Projectile
{
    public override void Start()
    {
        base.Start();
        speed = Mathf.RoundToInt(stats.AGILITY.GetValue() * 1.5f);
        rb.AddForce(transform.forward * speed, ForceMode.Impulse); // this has to be here, the speed variable in set only one row above
    }

    /// <summary>
    /// an IDEA to make the arrow behave normal upon touching a wall
    /// currenlty, it just drops to the floor, no matter what it hit
    /// fixed the collision matrix (forgot about that). now it collides with walls and enamies and me. ah, and floors too :D
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision col)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;

        if (col.gameObject.GetComponent<Stats>() && canDoDamage)
        {
            col.gameObject.GetComponent<Stats>().TakeDamage(stats.MAXDMG.GetValue());
        }

        canDoDamage = false;
    }
}