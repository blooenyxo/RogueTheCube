using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float globalCooldown = 0.5f;
    public GameObject enemyHolder;

    [HideInInspector] public Stats_Enemy[] stats_Enemy;

    private void Start()
    {
        SubscribeToEvents(); // setup so that we can count or see what types of enemies we kill
    }

    public void SubscribeToEvents()
    {
        if (enemyHolder != null)
        {
            stats_Enemy = enemyHolder.GetComponentsInChildren<Stats_Enemy>();
            foreach (Stats_Enemy se in stats_Enemy)
            {
                //se.onEnemyHealthChange += ;
                se.onEnemyDeath += EnemyDeath;
            }
        }
    }

    public void EnemyDeath(GameObject deadEnemy)
    {

    }
}