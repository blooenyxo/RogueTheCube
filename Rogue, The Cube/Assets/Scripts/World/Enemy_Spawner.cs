using UnityEngine;

/// <summary>
/// just a spawner. place these around the world, and use / reuse them for enemy management. 
/// maybe add in the future: a enemy array with multiple enemies to spawn a random one from a list of enemies.
/// </summary>
public class Enemy_Spawner : MonoBehaviour
{
    [Header("gameObjects")]
    public GameObject[] Enemies;
    public EnemyResourcePanel resourcePannel;

    [Header("ints")]
    public float respawnCooldown;
    public float spawnDistantce;

    [Header("bools")]
    public bool staticEnemy;
    public bool respawn = false;



    private GameObject spawnedGameObject;
    private int rnd;
    private GameObject player;
    private float time;
    private bool enemyDied;

    void Start()
    {
        if (Stats_Player.instance)
            player = Stats_Player.instance.gameObject;

        if (Enemies[rnd] != null)
        {
            SpawnGameObject();
        }
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            if (spawnedGameObject == null && !enemyDied)
            {
                time = Time.time + respawnCooldown;
                enemyDied = true;
            }

            if (spawnedGameObject == null && respawn && Vector3.Distance(this.transform.position, player.transform.position) > spawnDistantce && Time.time > time)
            {
                SpawnGameObject();
                resourcePannel.SubscribeToEvents();
                enemyDied = false;
            }
        }
        else
        {
            if (Stats_Player.instance)
                player = Stats_Player.instance.gameObject;
        }
    }

    private void SpawnGameObject()
    {
        rnd = Random.Range(0, Enemies.Length);
        spawnedGameObject = Instantiate(Enemies[rnd], this.transform.position, Quaternion.identity);
        spawnedGameObject.transform.SetParent(this.transform);
    }
}