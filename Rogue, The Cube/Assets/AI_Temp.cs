using UnityEngine;
using UnityEngine.AI;

public class AI_Temp : MonoBehaviour
{
    public bool pickNewLocation = false;
    private NavMeshAgent agent;
    private Vector3 spawnLocation;

    public float patrolRadius { get; private set; }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spawnLocation = transform.position;
        patrolRadius = 50f;
    }

    private void Update()
    {
        if (pickNewLocation == true)
        {
            PickLocation();
        }
    }

    private void PickLocation()
    {
        agent.SetDestination(spawnLocation + new Vector3(Random.Range(-patrolRadius, patrolRadius), transform.position.y, Random.Range(-patrolRadius, patrolRadius)));
        pickNewLocation = false;
    }
}
