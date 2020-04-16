using UnityEngine;

/// <summary>
/// just a spawner. place these around the world, and use / reuse them for enemy management. 
/// maybe add in the future: a enemy array with multiple enemies to spawn a random one from a list of enemies.
/// </summary>
public class Enemy_Spawner : MonoBehaviour
{
    public Enemy[] Enemies;

    public float minRespawnCooldown;

    public bool staticEnemy;
    public bool respawn;

    private EnemyResourcePanel enemyResourcePanel;
    private GameObject spawnedGameObject;
    private float time;

    Stats_Enemy se;

    void Start()
    {
        enemyResourcePanel = GameObject.Find("EnemyResourcesPanel").GetComponent<EnemyResourcePanel>();
        if (Enemies.Length > 0)
        {
            SpawnGameObject();
            enemyResourcePanel.SubscribeToEvents();
        }
    }

    private void LateUpdate()
    {
        if (spawnedGameObject == null && respawn && Time.time > time)
        {
            SpawnGameObject();
            enemyResourcePanel.SubscribeToEvents();
        }
    }

    private void SetRespawnTimer(GameObject deadEnemy)
    {
        time = Time.time + Random.Range(minRespawnCooldown, minRespawnCooldown * 2);
    }

    private void SpawnGameObject()
    {
        int rnd = Random.Range(0, Enemies.Length);

        spawnedGameObject = Instantiate(Enemies[rnd].baseEnemyGameObject, this.transform.position, Quaternion.identity);
        Instantiate(Enemies[rnd].visualModel, spawnedGameObject.transform);

        // the order is here important. maybe even add a delay here. This could all go into a coroutine.
        spawnedGameObject.transform.SetParent(this.transform);
        //spawnedGameObject.tag = "Enemy";
        spawnedGameObject.name = Enemies[rnd].title;

        spawnedGameObject.GetComponent<Stats_Enemy>().SetValues(Enemies[rnd]);
        spawnedGameObject.GetComponent<Controller_Equipment_Enemy>().SetValues(Enemies[rnd]);

        spawnedGameObject.GetComponent<Equipment_Visual_Enemy>().SetValues();
        spawnedGameObject.GetComponent<Equipment_Visual_Enemy>().EnemyUpdateVisuals();

        spawnedGameObject.GetComponent<LootDrop_Controller>().SetValues(Enemies[rnd]);

        spawnedGameObject.GetComponent<Controller_AI>().SetValues(Enemies[rnd]);
        spawnedGameObject.GetComponent<Controller_AI>().staticEnemy = staticEnemy;

        spawnedGameObject.GetComponentInChildren<Collision_Controller>().parentTag = Enemies[rnd].visualModel.tag;
        spawnedGameObject.GetComponentInChildren<Collision_Controller>().parentStats = spawnedGameObject.GetComponent<Stats>();

        se = spawnedGameObject.GetComponent<Stats_Enemy>();
        se.onEnemyDeath += SetRespawnTimer;
    }
}