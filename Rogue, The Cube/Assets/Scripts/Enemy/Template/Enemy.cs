using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Name / Description")]
    public string title;
    [TextArea(minLines: 2, maxLines: 4)]
    public string description;

    [Header("Stats")]
    public int HITPOINTS;
    public int MANAPOINTS;
    public int MOVESPEED;

    [Header("Enemy Types")]
    public bool contact;
    public bool melle;
    public bool ranged;
    public bool support;

    [Header("Gear")]
    public Item weapon;
    public Item offhand;
    public Item headGear;
    public Item chestGear;
}
