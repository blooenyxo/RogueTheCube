using UnityEngine;

public abstract class Controller_Weapon : MonoBehaviour
{
    [Header("Global Cooldown")]
    public float globalCooldown;

    [HideInInspector] public Stats stats;
    [HideInInspector] public string parentTag;
    [HideInInspector] public float cooldown;

    [Header("Components")]
    public Animator animator;

    public virtual void Start()
    {
        stats = GetComponentInParent<Stats>();
        parentTag = transform.parent.parent.tag;
    }

    public virtual void BaseAttack() { }

    public virtual void SpecialAttack() { }
}
