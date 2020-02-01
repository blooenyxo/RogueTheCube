using UnityEngine;

public class ParticleEffect_Controller : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(gameObject, GetComponentInChildren<ParticleSystem>().main.duration);
    }
}