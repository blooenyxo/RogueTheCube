using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public enum ITEMTYPE { HELMET, CHEST, WEAPON, OFFHAND, CONSUMABLE }
public enum ITEMCLASS { STRENGHT, INTELIGENCE, AGILITY }

[CreateAssetMenu (fileName = "New Item", menuName = "Item")]
/// <summary>
/// all the values that go on a item should be included in this class. this template will guide creating items for player and enemies.
/// most of the params here use descriptive names so no need of extra explaining.
/// </summary>
public class Item : ScriptableObject {
    public string ITEMNAME;
    public ITEMTYPE ITEM_TYPE;
    public ITEMCLASS ITEM_CLASS;
    [Header ("General Stats")]
    public int STRENGHT;
    public int INTELIGENCE;
    public int AGILITY;
    [Header ("For Weapons")]
    public int MINDMG;
    public int MAXDMG;
    [Header ("For Visuals")]
    public GameObject VISUAL_WEAPON;
    public GameObject VISUAL_ARMOR;
    public Sprite sprite;

    /// <summary>
    /// constructor. used to create random items at runtime
    /// </summary>
    /// <param name="itemname"></param>
    /// <param name="item_type"></param>
    /// <param name="strenght"></param>
    /// <param name="inteligence"></param>
    /// <param name="agility"></param>
    public Item (string itemname, ITEMTYPE item_type, int strenght, int inteligence, int agility, Sprite _sprite,
        GameObject visualWeapon, GameObject visualArmor) {
        ITEMNAME = itemname;
        ITEM_TYPE = item_type;
        STRENGHT = strenght;
        INTELIGENCE = inteligence;
        AGILITY = agility;

        sprite = _sprite;

        VISUAL_ARMOR = visualArmor;
        VISUAL_WEAPON = visualWeapon;
    }
}