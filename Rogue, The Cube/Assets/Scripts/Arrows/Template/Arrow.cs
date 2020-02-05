using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Arrow")]
public class Arrow : ScriptableObject
{
    [Header("Values")]
    public int speed;
    public int manaCost;

    [Header("Special Attributes")]
    public bool piercing;

    [Header("Visual Model")]
    public GameObject visualModel;
    [Header("Particle Effect")]
    public GameObject particleEffect;
    [Header("Buff / Debuff")]
    public Buff debuff;
}