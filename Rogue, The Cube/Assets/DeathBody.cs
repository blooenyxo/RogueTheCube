using UnityEngine;

public class DeathBody : MonoBehaviour
{
    public GameObject sphere;

    void Start()
    {
        Destroy(sphere, .5f);
        Destroy(gameObject, 5f);
    }
}
