using UnityEngine;

public abstract class Controller_Weapon : MonoBehaviour
{
    //[Header("Global Cooldown")]
    [HideInInspector] public Stats stats;
    [HideInInspector] public Controller_Equipment equipment;
    [HideInInspector] public string parentTag;
    [HideInInspector] public float cooldown;
    //[Header("Components")]
    [HideInInspector] public Animator animator;

    public virtual void Start()
    {
        stats = GetComponentInParent<Stats>();
        equipment = GetComponentInParent<Controller_Equipment>();
        parentTag = transform.parent.parent.tag;
    }

    public virtual void BaseAttack() { }

    public virtual void SpecialAttack() { }
}
