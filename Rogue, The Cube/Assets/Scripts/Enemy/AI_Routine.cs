using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATE { IDLE, MOVING, ATTACK, PICKLOCATION, INTERACT };
public enum AI_ROLE { OFFENSIVE, DEFENSIVE }

/// <summary>
/// this class controls all the behaviour the enemy character does. this should include only the basic stuff, applicable to all types of enemies.
/// </summary>
public class AI_Routine : MonoBehaviour
{
    public int detectionSphereRadius;
    public float stopingTime;

    public float globalColldown;
    private float cooldown = 0;

    [Header("Distance")]
    public float patrolRadius;
    public float attackDistance;

    [Header("Interaction")]
    public LayerMask targetLayer; // used for the overlap shpere interaction.
    //public string[] interactTags; // used to controll the interaction routine of the enemy. with whom to interact with
    private int _ignoreLayer = ~(1 << 15); // ignore shield when looking at player (ForwardRay())

    private NavMeshAgent agent;
    private Vector3 spawnLocation;
    //private bool spottedNearbyCharacter = false;
    private GameObject target;
    private bool newLocation = false;
    public float manaGain;
    private float _manaGain;
    public float staminaGain;
    private float _staminaGain;
    private float nextTime;

    public bool staticEnemy;
    public AI_STATE currentState;
    public AI_ROLE enemyRole;

    private void Start()
    {
        spawnLocation = this.transform.parent.position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = GetComponent<Stats>().MOVESPEED.GetValue();
        agent.angularSpeed = GetComponent<Stats>().MOVESPEED.GetValue() * 50f;
        currentState = AI_STATE.IDLE;
    }

    /// <summary>
    /// a basic state machine, using a switch statement. 
    /// on top of that, every check will also look for nearby interactions
    /// </summary>
    private void LateUpdate()
    {

        /*
        switch (currentState)
        {
            case AI_STATE.ATTACK:
                //Debug.Log("ATTACK");
                Attack();
                break;
            case AI_STATE.INTERACT:
                //Debug.Log("INTERACT");
                Interact();
                break;
            case AI_STATE.MOVING:
                //Debug.Log("MOVING");
                ArriveAtLocation();
                break;
            case AI_STATE.PICKLOCATION:
                //Debug.Log("PICKLOCATION");
                MoveToLocation(PickLocation());
                break;
            case AI_STATE.IDLE:
                //Debug.Log("IDLE");
                Idle();
                break;
        }
        */


        // Check Nearby
        switch (enemyRole)
        {
            case AI_ROLE.OFFENSIVE:
                List<GameObject> tmp_PlayerGameObjectsList = new List<GameObject>();
                tmp_PlayerGameObjectsList.AddRange(NearbyCharacters("Player"));

                if (tmp_PlayerGameObjectsList.Count > 0)
                {
                    target = tmp_PlayerGameObjectsList[0];
                    currentState = AI_STATE.ATTACK;
                }
                break;
            case AI_ROLE.DEFENSIVE:

                List<GameObject> tmp_FriendlyGameObjectsList = new List<GameObject>();
                tmp_FriendlyGameObjectsList.AddRange(NearbyCharacters("Enemy"));

                if (tmp_FriendlyGameObjectsList.Count > 0)
                {
                    foreach (GameObject friendlyEnemy in tmp_FriendlyGameObjectsList)
                    {
                        if (friendlyEnemy.GetComponent<Stats>().CurrentHealth < friendlyEnemy.GetComponent<Stats>().HITPOINTS.GetValue())
                        {
                            target = friendlyEnemy;
                            currentState = AI_STATE.INTERACT;
                            Interact();
                        }
                        else
                        {
                            target = null;
                            currentState = AI_STATE.IDLE;
                            Idle();
                        }
                    }
                }
                break;
            default:
                break;
        }

        #region Resource Management
        // resource management / gain back stamina and mana
        _staminaGain += 1 * Time.deltaTime;
        if (_staminaGain >= staminaGain)
        {
            GetComponent<Stats>().GainStamina(1);
            _staminaGain = 0f;
        }

        _manaGain += 1 * Time.deltaTime;
        if (_manaGain >= manaGain)
        {
            GetComponent<Stats>().GainMana(1);
            _manaGain = 0f;
        }
        #endregion

    }

    // Attack Routines
    private void NormalAttack()
    {
        if (GetComponentInChildren<Controller_Weapon>())
            GetComponentInChildren<Controller_Weapon>().BaseAttack();
    }
    private void SpecialAttack()
    {
        if (GetComponentInChildren<Controller_Weapon>())
            GetComponentInChildren<Controller_Weapon>().SpecialAttack();
    }
    private void UseOffhand()
    {
        if (Time.time > nextTime)
        {

            if (GetComponentInChildren<Controller_Offhand>())
            {
                GetComponentInChildren<Controller_Offhand>().target = target;
                GetComponentInChildren<Controller_Offhand>().UseOffhand();
            }

            nextTime = Time.time + GameManager.globalCooldown;

        }
    }

    /// <summary>
    /// function used to define what the enemy does in the idle state
    /// as for now, it just jumps to the PICKLOCATION state
    /// </summary>
    private void Idle()
    {
        //spottedNearbyCharacter = false;
        agent.isStopped = false;
        agent.SetDestination(spawnLocation);

        if (!staticEnemy)
        {
            currentState = AI_STATE.PICKLOCATION;
            MoveToLocation(PickLocation());
        }
    }

    /// <summary>
    /// find a new spot to walk to. here the check can be improved, so that the point is valind in any way needed
    /// </summary>
    /// <returns>returns the Vector3 position where the enemy will walk next</returns>
    private Vector3 PickLocation()
    {
        Vector3 currentWalkingLocation = spawnLocation + new Vector3(Random.Range(-patrolRadius, patrolRadius), transform.position.y, Random.Range(-patrolRadius, patrolRadius));
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
        agent.SetDestination(positionToMoveTo);
        currentState = AI_STATE.MOVING;
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
    /// returns a gameobject based on the surrounding characters, using a basic system to prioritise the player character above the rest
    /// this is not a great method. maybe figure a way to only use layers, and ignore tags.
    /// rethink this proces...
    /// update:
    /// this returns a list of gameobjects of given tags. i think this looks good now
    /// </summary>
    /// <returns>GameObject List of all nearby Characters based on a given tag</returns>
    private List<GameObject> NearbyCharacters(string _tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionSphereRadius, targetLayer);
        List<GameObject> gameObjectsInRangeOfGivenTag = new List<GameObject>();

        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag(_tag))
            {
                gameObjectsInRangeOfGivenTag.Add(col.gameObject);
            }
        }

        return gameObjectsInRangeOfGivenTag;
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
            if (ForwardRay() && Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
            {
                //agent.velocity = Vector3.zero;
                agent.isStopped = true;
                if (Time.time > cooldown)
                {
                    NormalAttack();
                    cooldown = Time.time + globalColldown;
                }
            }
            else if (!ForwardRay() || Vector3.Distance(transform.position, target.transform.position) > attackDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(target.transform.position);
                LookAt(target);
                //Debug.Log(agent.destination);
                //Debug.Log(Vector3.Distance(transform.position, target.transform.position));
            }
        }
        else if (target == null)
        {
            currentState = AI_STATE.IDLE;
            Idle();
        }
    }
    private void LookAt(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, (agent.angularSpeed) * Time.deltaTime);
    }

    private bool ForwardRay()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _ignoreLayer))
        {
            if (hit.transform.CompareTag("Player"))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
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
        // branch here if you need for multiple Enemy defensive actions
        UseOffhand();
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