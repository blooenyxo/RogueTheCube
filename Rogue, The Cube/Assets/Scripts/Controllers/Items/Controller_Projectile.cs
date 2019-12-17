using UnityEngine;

/// <summary>
/// main class for setting up a projectile (arrow, speel, trowable thigs later on)
/// </summary>
public class Controller_Projectile : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public int speed;
    // the layermask is set on the class where this one is inherited
    public LayerMask interactLayers;
    public float desctroyTimer;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        Destroy(this.gameObject, desctroyTimer);
    }
}
