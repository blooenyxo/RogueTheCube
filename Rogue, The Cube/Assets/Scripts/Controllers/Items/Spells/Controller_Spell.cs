using UnityEngine;

public class Controller_Spell : MonoBehaviour
{
    [HideInInspector] public Spell spell;
    [HideInInspector] public Stats casterStats;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public string parentTag;
    [HideInInspector] public int speed;
    [HideInInspector] public bool destroyOnWallhit = false;
    [HideInInspector] public bool damageOverTime;
    [HideInInspector] public float interval;
    [HideInInspector] public float nextTime;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        interval = spell.castInterval;
        destroyOnWallhit = spell.interactWithWalls;
        damageOverTime = spell.damageOverTime;
    }
}