using UnityEngine;

public class Spell : MonoBehaviour
{
    public string title;
    public int manaCost;
    public bool active;
    public bool channeled;
    public GameObject effect;

    public virtual bool CastSpell(Transform where, Stats casterStats, string parentTag) { return false; }
}
