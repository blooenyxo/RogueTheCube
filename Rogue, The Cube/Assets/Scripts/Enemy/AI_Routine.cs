using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE { IDLE, MOVING, ATTACK, PICKLOCATION, INTERACT };

/// <summary>
/// this class controls all the behaviour the enemy character does. this should include only the basic stuff, applicable to all types of enemies.
/// </summary>
public class AI_Routine : MonoBehaviour
{
    public AI_STATE currentState;
    public int detectionSphereRadius;
    public float stopingTime;

    [Header("Distance")]
    public float patrolRadius;
    public float attackDistance;

    [Header("Interaction")]
    public LayerMask targetLayer; // used for the overlap shpere interaction.
    //public string[] interactTags; // used to controll the interaction routine of the enemy. with whom to interact with
    private int _ignoreLayer = ~(1 << 15); // ignore shield when looking at player (ForwardRay())


    private NavMeshAgent agent;
    private Vector3 spawnLocation;
    private bool spottedNearbyCharacter = false;
    private GameObject target;
    private bool newLocation = false;

    private void Start()
    {
        spawnLocation = this.transform.parent.position;
        agent = GetComponent<NavMeshAgent>();
        currentState = AI_STATE.IDLE;
    }

    /// <summary>
    /// a basic state machine, using a switch statement. 
    /// on top of that, every check will also look for nearby interactions
    /// </summary>
    private void Update()
    {
        switch (currentState)
        {
            case AI_STATE.ATTACK:
                Attack();
                break;
            case AI_STATE.INTERACT:
                Interact();
                break;
            case AI_STATE.MOVING:
                ArriveAtLocation();
                break;
            case AI_STATE.PICKLOCATION:
                MoveToLocation(PickLocation());
                break;
            case AI_STATE.IDLE:
                Idle();
                break;
            default:

                break;
        }

        CheckNearby();
    }

    /// <summary>
    /// function used to define what the enemy does in the idle state
    /// as for now, it just jumps to the PICKLOCATION state
    /// </summary>
    private void Idle()
    {
        spottedNearbyCharacter = false;
        agent.isStopped = false;
        currentState = AI_STATE.PICKLOCATION;
    }

    /// <summary>
    /// find a new spot to walk to. here the check can be improved, so that the point is valind in any way needed
    /// </summary>
    /// <returns>returns the Vector3 position where the enemy will walk next</returns>
    private Vector3 PickLocation()
    {
        Vector3 currentWalkingLocation = spawnLocation + new Vector3(UnityEngine.Random.Range(-patrolRadius, patrolRadius), -3.9f, Random.Range(-patrolRadius, patrolRadius));
        return currentWalkingLocation;
    }

    /// <summary>
    /// set the agent destination to the new position to move to
    /// </summary>
    /// <param name="positionToMoveTo"></param>
    private void MoveToLocation(Vector3 positionToMoveTo)
    {
        newLocation = false;
        agent.stoppingDistance = 0f;
        currentState = AI_STATE.MOVING;
        agent.SetDestination(positionToMoveTo);
    }

    /// <summary>
    /// used in the walking routine. after reaching the destination, this function redirects the AI State to the PICKLOCATION state.
    /// - coroutine for a short break at the destination
    /// </summary>
    private void ArriveAtLocation()
    {
        if (agent.remainingDistance <= .5f && newLocation == false)
        {
            StartCoroutine(NewLocation());
        }
    }

    private IEnumerator NewLocation()
    {
        newLocation = true;
        yield return new WaitForSeconds(stopingTime);
        currentState = AI_STATE.PICKLOCATION;
    }

    /// <summary>
    /// set the GameObject target to a value, based on the surrounding characters using the NearbyCharacters() function
    /// </summary>
    private void CheckNearby()
    {
        target = NearbyCharacters("Player");

        if (target)
            currentState = AI_STATE.ATTACK;

        if (target == null && spottedNearbyCharacter == true)
        {
            currentState = AI_STATE.IDLE;
        }


    }

    /// <summary>
    /// returns a gameobject based on the surrounding characters, using a basic system to prioritise the player character above the rest
    /// this is not a great method. maybe figure a way to only use layers, and ignore tags.
    /// rethink this proces...
    /// </summary>
    /// <returns>GameObject used to be stored into the target variable</returns>
    private GameObject NearbyCharacters(string _tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionSphereRadius, targetLayer);

        if (hitColliders.Length > 0)
        {
            foreach (Collider col in hitColliders)
            {
                if (col.CompareTag(_tag))
                {
                    spottedNearbyCharacter = true;
                    return col.gameObject;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// this needs work. the enemy will attack constantly. fast. does now work well with a arrow / ranged enemy
    /// later edit: this was fixed with a global cooldown in the weapon_controller
    /// </summary>
    private void Attack()
    {
        agent.stoppingDistance = attackDistance;

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            LookAt(target);

            if (ForwardRay() && Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
            {
                if (GetComponentInChildren<Controller_Weapon>())
                    GetComponentInChildren<Controller_Weapon>().BaseAttack();
            }
        }
        else if (target == null)
            currentState = AI_STATE.IDLE;

        spottedNearbyCharacter = false;
    }
    private void LookAt(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (agent.angularSpeed / 50) * Time.deltaTime);
    }

    private bool ForwardRay()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _ignoreLayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
                return true;
            }
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    private void Interact()
    {
        agent.stoppingDistance = attackDistance;

        if (target != null)
        {
            agent.SetDestination(target.transform.position);
            LookAt(target);
        }
        else if (target == null)
            currentState = AI_STATE.IDLE;
    }

    public void Stop()
    {
        agent.isStopped = true;
        currentState = AI_STATE.IDLE;
    }

    /// <summary>
    /// editor stuff
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
    }
}