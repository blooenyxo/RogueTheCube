using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Name / Description")]
    public string title;
    [TextArea(minLines: 2, maxLines: 4)]
    public string description;

    [Header("Stats")]
    public int STRENGHT;
    public int INTELIGENCE;
    public int AGILITY;
    public int HITPOINTS;
    public int MANAPOINTS;
    public int MOVESPEED;
    public int STAMINA;

    [Header("Gear")]
    public Item headGear;
    public Item chestGear;
    public Item weapon;
    public Item offhand;

    [Header("Visual Model")]
    public GameObject baseEnemyGameObject;
    public GameObject visualModel;

    [Header("Loot")]
    public int maxNrLowTierItems;
    public int maxNrMediumTierItems;
    public int maxNrHighTierItems;
    public int maxNrConsumableItems;
    public int minNrConsumableStacks;
    public int maxNrConsumableStacks;
    public int maxNrSpecialItems;
    public int minGoldStacks;
    public int maxGoldStacks;
    public int chanceToDropLoot;

    [Header("Enemy AI")]
    public EnemyType enemyType;
    public float detectionSphereRadius;
    public float attackingDistance;
    [Header("Stamina Gain")]
    public float staminaEverySeconds;
    public int staminGainAmmount;
    [Header("Mana Gain")]
    public float manaEverySeconds;
    public int manaGainAmmount;
}