using UnityEngine;

public enum ITEMTYPE { HELMET, CHEST, WEAPON, OFFHAND, CONSUMABLE, ARROW, SPELL }
public enum ITEMCLASS { STRENGHT, INTELIGENCE, AGILITY, NONE }

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Item")]
/// <summary>
/// all the values that go on a item should be included in this class. this template will guide creating items for player and enemies.
/// most of the params here use descriptive names so no need of extra explaining.
/// </summary>
public class Item : ScriptableObject
{
    public string ITEMNAME;
    [TextArea(minLines: 2, maxLines: 4)]
    public string DESCRIPTION;
    public ITEMTYPE ITEM_TYPE;
    public ITEMCLASS ITEM_CLASS;
    [Header("General Stats")]
    public int STRENGHT;
    public int INTELIGENCE;
    public int AGILITY;
    [Header("For Weapons")]
    public int MINDMG;
    public int MAXDMG;
    public int MINMAGIC;
    public int MAXMAGIC;
    public float globalCooldown;
    [Header("Consumables Section")]
    public int Health;
    public int Mana;
    [Header("For Visuals")]
    public GameObject VISUAL_MODEL;
    public GameObject VISUAL_ARMOR;
    public Sprite sprite;
    [Header("For Item Stacking")]
    public bool stackable;
    [Header("2H Weapon")]
    public bool TwoHandetWeapon;
    [Header("For arrows, add a Arrow SO here")]
    public Arrow arrow;
    [Header("if this item can cast a spell")]
    public Spell spell;

    /// <summary>
    /// constructor. used to create random items at runtime
    /// </summary>
    public Item(string itemname, ITEMTYPE item_type, int strenght, int inteligence, int agility, Sprite _sprite,
        GameObject visualWeapon, GameObject visualArmor)
    {
        ITEMNAME = itemname;
        ITEM_TYPE = item_type;
        STRENGHT = strenght;
        INTELIGENCE = inteligence;
        AGILITY = agility;

        sprite = _sprite;

        VISUAL_ARMOR = visualArmor;
        VISUAL_MODEL = visualWeapon;
    }
}