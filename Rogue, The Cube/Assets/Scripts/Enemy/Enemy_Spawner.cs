using UnityEngine;

/// <summary>
/// just a spawner. place these around the world, and use / reuse them for enemy management. 
/// maybe add in the future: a enemy array with multiple enemies to spawn a random one from a list of enemies.
/// </summary>
public class Enemy_Spawner : MonoBehaviour
{
    public GameObject[] Enemies;
    public float spawnDistantce;

    private GameObject spawnedGameObject;
    private int rnd;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (Enemies[rnd] != null)
        {
            SpawnGameObject();
        }
    }

    private void LateUpdate()
    {

        if (spawnedGameObject == null)
        {

            SpawnGameObject();
        }
    }

    private void SpawnGameObject()
    {
        if (player != null && Vector3.Distance(this.transform.position, player.transform.position) > spawnDistantce)
        {
            rnd = Random.Range(0, Enemies.Length);
            spawnedGameObject = Instantiate(Enemies[rnd], this.transform.position, Quaternion.identity);
            spawnedGameObject.transform.SetParent(this.transform);
        }
    }
}