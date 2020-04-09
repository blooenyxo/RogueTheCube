using UnityEngine;

public abstract class Controller_Weapon : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public Stats parentStats;
    [HideInInspector] public string parentTag;
    [HideInInspector] public Rigidbody parentRigidbody;
    [HideInInspector] public Controller_Equipment equipment;

    public int staminaUse;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        parentStats = GetComponentInParent<Stats>();
        equipment = GetComponentInParent<Controller_Equipment>();
        parentTag = transform.parent.tag; // stupid, but works
        parentRigidbody = transform.parent.parent.parent.GetComponent<Rigidbody>(); // same
    }

    public virtual void BaseAttack() { }

    public virtual void SpecialAttack() { }
}
