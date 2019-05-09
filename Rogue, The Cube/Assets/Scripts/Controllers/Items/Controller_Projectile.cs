using UnityEngine;

public class Controller_Projectile : MonoBehaviour
{
    private Rigidbody rb;

    public int speed;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        Destroy(this.gameObject, 5f);
    }
}
