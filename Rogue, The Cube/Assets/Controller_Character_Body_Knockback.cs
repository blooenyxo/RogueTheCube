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
        if (distance < 1)
            distance = 1;

        float forceModifier = (maxKnockbackDistance - distance) * .1f;

        knockBackForce *= forceModifier;

        parentRigidBody.AddForce((attacker.forward + transform.up) * knockBackForce, ForceMode.Impulse);
        knockBackForce = _knockBackForce;
    }
}