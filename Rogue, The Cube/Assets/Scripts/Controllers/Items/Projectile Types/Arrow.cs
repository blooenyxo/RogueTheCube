using UnityEngine;

public class Arrow : Controller_Projectile
{
    private Stats_Player stats_Player;

    public override void Start()
    {
        stats_Player = Stats_Player.instance;

        speed = Mathf.RoundToInt(stats_Player.AGILITY.GetValue() * 1.5f);
        base.Start();
    }

    /// <summary>
    /// an IDEA to make the arrow behave normal upon touching a wall
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter(Collision col)
    {
        // to compare the topmost bits of the layerMask value you need to bitshift (<<) by 1. this works right now as is, 
        // maybe update later to a more classy sollution.
        if (1 << col.gameObject.layer == interactLayers.value)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
        }
    }
}