using UnityEngine;
using UnityEngine.AI;

public class Controller_Enemy : MonoBehaviour
{
    public Item enemyWeapon;
    public Item enemyArmor;

    private NavMeshAgent agent;
    private Stats stats;

    private void Start()
    {
        stats = GetComponent<Stats>();
        agent = GetComponent<NavMeshAgent>();

        agent.speed = stats.MOVESPEED.GetValue();
        agent.angularSpeed = Mathf.CeilToInt(stats.MOVESPEED.GetValue() * 1.5f);
    }
}