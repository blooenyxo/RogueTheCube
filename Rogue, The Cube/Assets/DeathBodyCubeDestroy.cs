using UnityEngine;

public class DeathBodyCubeDestroy : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, Random.Range(.5f, 5f));
    }
}
