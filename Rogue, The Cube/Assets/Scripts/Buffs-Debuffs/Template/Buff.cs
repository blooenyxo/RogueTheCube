using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Buff")]
public class Buff : ScriptableObject
{
    [Header("Title")]
    public string title;

    [Header("Values")]
    public int buffValue;
    public bool harmfull;
    public bool healing;
    public bool damage;
    public bool movementImpairing;
    public int movementImpairingValue;

    [Header("How Long")]
    public float duration;

    [Header("Ticks per second")]
    public float interval;

    [Header("Visuals")]
    public GameObject visualEffect;
    public Sprite sprite;
}