using UnityEngine;

public abstract class Controller_Weapon : MonoBehaviour
{
    public Stats stats;

    public virtual void Start()
    {
        if (transform.parent.parent.CompareTag("Player"))
            stats = Stats_Player.instance;

        if (transform.parent.parent.CompareTag("Enemy"))
            stats = this.transform.parent.parent.GetComponent<Stats_Enemy>();
    }

    public virtual void BaseAttack()
    {

    }

    public virtual void SpecialAttack() { }
}
