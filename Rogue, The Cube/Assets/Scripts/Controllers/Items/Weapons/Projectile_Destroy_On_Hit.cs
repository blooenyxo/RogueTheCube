using UnityEngine;

public class Projectile_Destroy_On_Hit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        Destroy(gameObject);
    }
}
