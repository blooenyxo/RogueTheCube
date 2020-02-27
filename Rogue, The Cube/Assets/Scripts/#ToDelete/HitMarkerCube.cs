using UnityEngine;

public class HitMarkerCube : MonoBehaviour
{
    private Rigidbody rb;
    private HitMarker hm;
    void Start()
    {
        hm = GetComponentInParent<HitMarker>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(-transform.forward, ForceMode.Impulse);
        rb.velocity = Random.onUnitSphere * hm.explosionSpeed;
        Destroy(gameObject, Random.Range(hm.minDespawnTime, hm.maxDespawnTime));
    }
}
