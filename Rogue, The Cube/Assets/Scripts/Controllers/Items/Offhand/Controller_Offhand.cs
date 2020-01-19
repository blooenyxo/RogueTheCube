using UnityEngine;

public abstract class Controller_Offhand : MonoBehaviour
{
    [HideInInspector] public BoxCollider shieldCollider;

    public virtual void Start()
    {
        shieldCollider = GetComponentInChildren<BoxCollider>();
        shieldCollider.enabled = false;
    }

    public virtual void UseOffhand() { }

    public virtual void ReleaseOffhand() { }
}
