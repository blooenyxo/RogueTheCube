using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// this is a mess :D 
/// the way i made it, was storing everyhing localy on every item. i dont think this is the best way, but it works
/// when you right click on item, they move acording to the unwritten law of rpg doing :D from loot window to inventory, 
/// from inventory to equipment, and from equipment to inventory. There is no logic for what happens if slot is ocupied.
/// later edit: made the swaping mechanism.
/// </summary>
public class Item_Click : MonoBehaviour, IPointerDownHandler
{
    private Transform[] inventorySlots;
    private Transform[] equipmentSlots;

    private GameObject inv;
    private GameObject equ;

    private Item_UI thisItem_UI;

    [HideInInspector] public Transform Button1;
    [HideInInspector] public Transform Button2;
    [HideInInspector] public Transform Button3;
    [HideInInspector] public Transform ButtonQ;


    private void Start()
    {
        // I store the parents localy
        inv = GameObject.Find("Inventory");
        equ = GameObject.Find("EquipmentBackground");

        Button1 = GameObject.FindGameObjectWithTag("Button1").transform;
        Button2 = GameObject.FindGameObjectWithTag("Button2").transform;
        Button3 = GameObject.FindGameObjectWithTag("Button3").transform;
        ButtonQ = GameObject.FindGameObjectWithTag("ButtonQ").transform;

        // I make a Transform array, with the size of the child count. easy
        inventorySlots = new Transform[28];
        equipmentSlots = new Transform[equ.transform.childCount];


        // I loop through all the children and store them individually as transforms
        for (int i = 0; i < 24; i++)
            inventorySlots[i] = inv.transform.GetChild(i);

        inventorySlots[24] = Button1;
        inventorySlots[25] = Button2;
        inventorySlots[26] = Button3;
        inventorySlots[27] = ButtonQ;


        for (int j = 0; j < equ.transform.childCount; j++)
            equipmentSlots[j] = equ.transform.GetChild(j);

        thisItem_UI = GetComponent<Item_UI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("item was in " + transform.parent.tag);
            MoveItem(transform.parent.tag); // do something based on the current parent tag
        }
    }

    public void MoveItem(string _str)
    {
        if (_str == "EquipmentSlot")
        {
            //move to inventory if enough free space
            RemoveFromEquipment(-1);
            MoveToInventory();
            if (equipmentSlots[3].GetComponentInChildren<Item_Click>())
                equipmentSlots[3].GetComponentInChildren<Item_Click>().MoveToInventory();
        }
        else if (_str == "InventorySlot")
        {
            //move to equipment / switch item if equipment slot already ocupied
            SortFromInventoryToEquipment(thisItem_UI.item.ITEM_TYPE);
        }
        else if (_str == "LootSlot")
        {
            if (!LookForSimilarItems())
            {
                MoveToInventory();
            }
            //move to inventory if enough free space
            // the next line is for clearing the lootbox content after item was removed
            if (GameObject.Find("Player").GetComponent<Controller_Input>().NearbyInteraction())
                GameObject.Find("Player").GetComponent<Controller_Input>().NearbyInteraction().GetComponent<LootBox_Controller>().RemoveItemFromList(thisItem_UI.item);

        }
        else if (_str == "Button1" || _str == "Button2" || _str == "Button3" || _str == "ButtonQ")
        {
            SortFromInventoryToEquipment(thisItem_UI.item.ITEM_TYPE);
        }
    }

    void SortFromInventoryToEquipment(ITEMTYPE _itemType)
    {
        if (_itemType == ITEMTYPE.HELMET)
        {
            if (equipmentSlots[0].childCount == 0) // the index I use here is from the Equipment.instance array. In it we always use                                                   
            {                                      // the same indexes (0 - Helmet 1 - Chest 2 - Weapon 3 - Offhand)
                EquipToEquipment(0);
            }
            else
            {
                SwapWithEquipment(0);
            }
        }

        else if (_itemType == ITEMTYPE.CHEST)
        {
            if (equipmentSlots[1].childCount == 0)
            {
                EquipToEquipment(1);
            }
            else
            {
                SwapWithEquipment(1);
            }
        }
        else if (_itemType == ITEMTYPE.WEAPON)
        {
            if (equipmentSlots[2].childCount == 0)
            {
                EquipToEquipment(2);
            }
            else
            {
                SwapWithEquipment(2);
            }
        }
        else if (_itemType == ITEMTYPE.OFFHAND)
        {
            if (equipmentSlots[3].childCount == 0)
            {
                EquipToEquipment(3);
            }
            else
            {
                SwapWithEquipment(3);
            }
        }
        else if (_itemType == ITEMTYPE.ARROW)
        {
            if (equipmentSlots[2].childCount != 0f)
            {
                if (equipmentSlots[2].GetComponentInChildren<Item_UI>().item.ITEM_CLASS == ITEMCLASS.AGILITY)
                {
                    if (equipmentSlots[3].childCount == 0)
                    {
                        EquipToEquipment(3);
                    }
                    else
                    {
                        if (thisItem_UI.item == equipmentSlots[3].GetComponentInChildren<Item_UI>().item)
                        {
                            equipmentSlots[3].GetComponentInChildren<Item_UI>().stacks += thisItem_UI.stacks;
                            equipmentSlots[3].GetComponentInChildren<Item_UI>().AdjustStackText();
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            SwapWithEquipment(3);
                        }
                    }
                }
            }
        }
        else if (_itemType == ITEMTYPE.SPELL)
        {
            if (equipmentSlots[2].childCount != 0f)
            {
                if (equipmentSlots[2].GetComponentInChildren<Item_UI>().item.ITEM_CLASS == ITEMCLASS.INTELIGENCE)
                {
                    if (equipmentSlots[3].childCount == 0)
                    {
                        EquipToEquipment(3);
                    }
                    else
                    {
                        SwapWithEquipment(3);
                    }
                }
            }
        }
        else if (_itemType == ITEMTYPE.CONSUMABLE)
        {
            bool gainedHealth = false;
            bool gainedMana = false;
            if (thisItem_UI.item.Health > 0f)
            {
                if (Stats_Player.instance.CurrentHealth < Stats_Player.instance.HITPOINTS.GetValue())
                {
                    Stats_Player.instance.Heal(thisItem_UI.item.Health);
                    gainedHealth = true;
                }
            }
            if (transform.GetComponent<Item_UI>().item.Mana > 0f)
            {
                if (Stats_Player.instance.CurrentMana < Stats_Player.instance.MANAPOINTS.GetValue())
                {
                    Stats_Player.instance.GainMana(thisItem_UI.item.Mana);
                    gainedMana = true;
                }
            }

            if (gainedHealth || gainedMana)
            {
                transform.GetComponent<Item_UI>().stacks--;
                thisItem_UI.AdjustStackText();
                if (thisItem_UI.stacks <= 0)
                    Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// using the passed index we sort the item to its place
    /// </summary>
    void EquipToEquipment(int i)
    {
        equipmentSlots[i].GetComponent<Item_Drop_Eq>().localStoredItem = thisItem_UI.item;
        equipmentSlots[i].GetComponent<Item_Drop_Eq>().EquipItem(this.gameObject);
        transform.SetParent(equipmentSlots[i]);
    }

    /// <summary>
    /// this is one way to do it :)))
    /// </summary>
    void RemoveFromEquipment(int i)
    {
        if (i == -1) // when equipment slot is empty
            equipmentSlots[transform.GetComponentInParent<Item_Drop_Eq>().index].GetComponent<Item_Drop_Eq>().RemoveItem();
        else         // when eqiupment slot is filled and we swap items
            equipmentSlots[i].GetComponent<Item_Drop_Eq>().RemoveItem();
    }

    bool LookForSimilarItems()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].childCount > 0)
            {
                if (inventorySlots[i].transform.GetComponentInChildren<Item_UI>().item == thisItem_UI.item)
                {
                    //transform.SetParent(inventorySlots[i]);
                    if (inventorySlots[i].transform.GetComponentInChildren<Item_UI>().item.stackable)
                    {
                        inventorySlots[i].transform.GetComponentInChildren<Item_UI>().stacks += thisItem_UI.stacks;
                        inventorySlots[i].transform.GetComponentInChildren<Item_UI>().AdjustStackText();
                        thisItem_UI.stacks -= thisItem_UI.stacks;
                        if (thisItem_UI.stacks <= 0)
                        {
                            Destroy(gameObject);
                            return true;
                        }
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void MoveToInventory()
    {
        for (int i = inventorySlots.Length - 1; i >= 0; i--)
        {
            if (inventorySlots[i].childCount == 0)
            {
                // move here
                transform.SetParent(inventorySlots[i]);

                //inventorySlots[i].GetComponent<Item_UI>().item.stacks++;
                //if (GetComponent<Item_UI>().item.stacks <= 0)
                //{
                //    Destroy(gameObject);
                //}
            }
            else
            {
                // not enough room
            }
        }
    }

    void SwapWithEquipment(int i)
    {
        RemoveFromEquipment(i);
        equipmentSlots[i].GetChild(0).SetParent(this.transform.parent); // for sorting back into the inventory, and not stacking
        EquipToEquipment(i);
    }

    void SwapWithInventory(int i)
    {

    }
}