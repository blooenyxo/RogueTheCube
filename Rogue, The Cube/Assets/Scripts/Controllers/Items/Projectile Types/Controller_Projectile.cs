using UnityEngine;

/// <summary>
/// main class for setting up a projectile (arrow, spell, trowable thigs later on)
/// </summary>
public abstract class Controller_Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public int speed;
    [HideInInspector] public Stats stats;
    [HideInInspector] public bool canDoDamage = true;
    [HideInInspector] public string parentTag;
    [HideInInspector] public Buff buff;
    [HideInInspector] public bool destroyOnWallhit = false;
    [HideInInspector] public bool damageOverTime;
    [HideInInspector] public float interval;
    [HideInInspector] public float nextTime;

    // the layermask is set on the class where this one is inherited
    public LayerMask interactLayers;
    public float desctroyTimer;

    public virtual void Start()
    {
        speed = 0;
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, desctroyTimer);
    }
}
