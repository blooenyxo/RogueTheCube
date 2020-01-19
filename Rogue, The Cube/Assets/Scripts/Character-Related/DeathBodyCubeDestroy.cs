using UnityEngine;

public class DeathBodyCubeDestroy : MonoBehaviour
{
    private Rigidbody rb;
    private DeathBody db;

    void Start()
    {
        db = GetComponentInParent<DeathBody>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Random.onUnitSphere * db.explosionSpeed;
        Destroy(gameObject, Random.Range(db.minDespawnTime, db.maxDespawnTime));
    }
}
