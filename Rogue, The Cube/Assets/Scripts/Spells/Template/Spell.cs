using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Spell")]

public class Spell : ScriptableObject
{
    [Header("Name")]
    public string title;

    [Header("Values")]
    public int manaCost;
    public bool attacking;
    public bool healing;
    public bool channeled;
    public bool interactWithWalls;

    [Header("Visual Model")]
    public GameObject visualModel;

    [Header("Buff / Debuff")]
    public Buff buff;

    //[Header("Visual")]
    //public GameObject effect;
}
