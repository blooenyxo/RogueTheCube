using UnityEngine;

/// <summary>
/// main class for setting up a projectile (arrow, spell, trowable thigs later on)
/// </summary>
public abstract class Controller_Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public int speed = 0;
    [HideInInspector] public Stats stats;
    [HideInInspector] public bool canDoDamage = true;
    [HideInInspector] public string parentTag;
    [HideInInspector] public Buff buff;
    [HideInInspector] public bool destroyOnWallhit = false;

    // the layermask is set on the class where this one is inherited
    public LayerMask interactLayers;
    public float desctroyTimer;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, desctroyTimer);
    }
}
