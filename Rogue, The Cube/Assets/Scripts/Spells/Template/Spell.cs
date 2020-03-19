using UnityEngine;

public enum SpellType { attacking, healing, protection }
public enum SpellTarget { other, self }
public enum SpellMode { normal, channeled }

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Spell")]
public class Spell : ScriptableObject
{
    [Header("Name")]
    public string title;

    [Header("Type of Spell")]
    public SpellType spellType;
    public SpellTarget spellTarget;
    public SpellMode spellMode;

    [Header("Values")]
    public int manaCost;
    public bool interactWithWalls;
    public float castInterval;

    [Header("Damage Over Time")]
    public bool damageOverTime;
    public float interval;

    [Header("Visual Model")]
    public GameObject visualModel;

    [Header("Buff / Debuff")]
    public Buff buff;

    //[Header("Visual")]
    //public GameObject effect;
}
