using UnityEngine;

public abstract class Controller_Offhand : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    [HideInInspector] public string parentTag;
    [HideInInspector] public Stats stats;

    public virtual void Start()
    {
        parentTag = gameObject.transform.parent.parent.tag;
        //this.transform.GetChild(0).tag = parentTag;
        stats = GetComponentInParent<Stats>();
    }

    public virtual void UseOffhand() { }

    public virtual void ReleaseOffhand() { }
}