using UnityEngine;

public abstract class Controller_Offhand : MonoBehaviour
{
    public string parentTag;

    public virtual void Start()
    {
        parentTag = gameObject.transform.parent.tag;
    }

    public virtual void UseOffhand() { }

    public virtual void ReleaseOffhand() { }
}