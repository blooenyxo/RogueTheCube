using UnityEngine;

public class Controller_Character_Body_Knockback : MonoBehaviour
{
    private Rigidbody parentRigidBody;
    public float knockBackForce;

    private float _knockBackForce;
    private float maxKnockbackDistance = 30;

    void Start()
    {
        parentRigidBody = transform.parent.GetComponent<Rigidbody>();
        _knockBackForce = knockBackForce;
    }

    public void Knockback(Transform attacker)
    {
        float distance = Vector3.Distance(this.transform.position, attacker.position);
        float forceModifier = (maxKnockbackDistance - distance) * .4f;

        knockBackForce *= forceModifier;

        parentRigidBody.AddForce(attacker.forward * knockBackForce, ForceMode.Impulse);
        parentRigidBody.AddForce(attacker.up * 2f, ForceMode.Impulse);
        knockBackForce = _knockBackForce;
    }
}