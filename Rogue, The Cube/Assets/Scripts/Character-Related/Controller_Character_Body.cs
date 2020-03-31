using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Controller_Character_Body : MonoBehaviour
{
    [Header("Equipment Slots")]
    public GameObject headPoint;
    public GameObject chestPoint;
    public GameObject rightHandPoint;
    public GameObject leftHandPoint;

    public GameObject deathBody;

    private Rigidbody parentRB;
    private float timeToWait = 1f;
    private float knockBackForce = 3f;

    private void Start()
    {
        parentRB = transform.parent.GetComponent<Rigidbody>();
    }

    public void KnockBack(Vector3 _forceDirection)
    {
        parentRB.AddForce(knockBackForce * _forceDirection, ForceMode.Impulse);
        parentRB.AddForce(knockBackForce * transform.up, ForceMode.Impulse);
    }

    private IEnumerator KnockBackEffect()
    {
        NavMeshAgent agent = GetComponentInParent<NavMeshAgent>();
        agent.isStopped = true;
        agent.enabled = false;
        yield return new WaitForSeconds(timeToWait);
        agent.isStopped = false;
        agent.enabled = true;
    }
}
