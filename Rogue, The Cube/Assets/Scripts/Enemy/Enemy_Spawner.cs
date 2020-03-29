using UnityEngine;

/// <summary>
/// just a spawner. place these around the world, and use / reuse them for enemy management. 
/// maybe add in the future: a enemy array with multiple enemies to spawn a random one from a list of enemies.
/// </summary>
public class Enemy_Spawner : MonoBehaviour
{
    [Header("Enemy Array")]
    public Enemy[] Enemies;

    [Header("ints")]
    public float respawnCooldown;
    public float spawnDistantce;

    [Header("bools")]
    public bool staticEnemy;
    public bool respawn = false;

    private EnemyResourcePanel enemyResourcePanel;
    private GameObject spawnedGameObject;
    private int rnd;
    private GameObject player;
    private float time;
    private bool enemyDied;

    void Start()
    {
        if (Stats_Player.instance)
            player = Stats_Player.instance.gameObject;

        enemyResourcePanel = GameObject.Find("EnemyResourcesPanel").GetComponent<EnemyResourcePanel>();

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
                enemyResourcePanel.SubscribeToEvents();
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

        spawnedGameObject = Instantiate(Enemies[rnd].baseEnemyGameObject, this.transform.position, Quaternion.identity);
        Instantiate(Enemies[rnd].visualModel, spawnedGameObject.transform);

        // the order is here important. maybe even add a delay here. This could all go into a coroutine.
        spawnedGameObject.transform.SetParent(this.transform);
        //spawnedGameObject.tag = "Enemy";
        spawnedGameObject.name = Enemies[rnd].title;

        spawnedGameObject.GetComponent<Stats_Enemy>().enemy = Enemies[rnd];
        spawnedGameObject.GetComponent<Stats_Enemy>().SetValues();

        spawnedGameObject.GetComponent<Controller_Equipment_Enemy>().enemy = Enemies[rnd];
        spawnedGameObject.GetComponent<Controller_Equipment_Enemy>().SetValues();

        spawnedGameObject.GetComponent<Equipment_Visual_Enemy>().SetValues();
        spawnedGameObject.GetComponent<Equipment_Visual_Enemy>().EnemyUpdateVisuals();

        spawnedGameObject.GetComponent<LootDrop_Controller>().enemy = Enemies[rnd];
        spawnedGameObject.GetComponent<LootDrop_Controller>().SetValues();

        spawnedGameObject.GetComponent<Controller_AI>().enemy = Enemies[rnd];
        spawnedGameObject.GetComponent<Controller_AI>().SetValues();
        spawnedGameObject.GetComponent<Controller_AI>().staticEnemy = staticEnemy;
    }
}