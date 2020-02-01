using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string title;
    public int manaCost;
    public bool targetSelf;
    public bool active;
    public bool channeled;
    public GameObject effect;

    public virtual void CastSpell(Transform where, Stats casterStats, string parentTag) { }
}
