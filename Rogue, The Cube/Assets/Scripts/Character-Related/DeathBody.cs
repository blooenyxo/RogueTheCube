using UnityEngine;

public class DeathBody : MonoBehaviour
{
    public float minDespawnTime;
    public float maxDespawnTime;

    public float explosionSpeed;
    void Start()
    {
        Destroy(gameObject, maxDespawnTime);
    }
}
