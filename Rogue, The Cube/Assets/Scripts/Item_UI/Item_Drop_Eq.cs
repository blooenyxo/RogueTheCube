using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Item_Drop_Eq : Item_Drop
{
    private Image image;
    private Equipment equipment;
    public Item localStoredItem;
    public int index;

    private void Start()
    {
        if (Stats_Player.instance)
            equipment = Stats_Player.instance.gameObject.GetComponent<Equipment>();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (equipment == null)
            equipment = Stats_Player.instance.gameObject.GetComponent<Equipment>();

        // this logic handles what happens if after picking an item off a equipmentslot you decide to place it right back down.
        if (transform.childCount == 0)
        {
            foreach (ITEMTYPE type in acceptedItemType)
            {
                if (eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == type)
                {
                    if (eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == ITEMTYPE.ARROW)
                    {
                        if (equipment.currentEquipment[2] != null)
                        {
                            if (equipment.currentEquipment[2].ITEM_CLASS == ITEMCLASS.AGILITY)
                            {
                                if (localStoredItem != eventData.pointerDrag.GetComponent<Item_UI>().item)
                                {
                                    localStoredItem = eventData.pointerDrag.GetComponent<Item_UI>().item;
                                    EquipItem(eventData.pointerDrag.gameObject);
                                }
                                else if (localStoredItem == eventData.pointerDrag.GetComponent<Item_UI>().item)
                                {
                                    equipment.currentEquipmentGameObjects[3].GetComponent<Item_UI>().stacks++;
                                }

                            }
                            base.OnDrop(eventData);
                        }
                        return;
                    }
                    else if (eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == ITEMTYPE.SPELL)
                    {
                        if (equipment.currentEquipment[2] != null)
                        {
                            if (equipment.currentEquipment[2].ITEM_CLASS == ITEMCLASS.INTELIGENCE)
                            {
                                if (localStoredItem != eventData.pointerDrag.GetComponent<Item_UI>().item)
                                {
                                    localStoredItem = eventData.pointerDrag.GetComponent<Item_UI>().item;
                                }
                                EquipItem(eventData.pointerDrag.gameObject);
                            }
                            base.OnDrop(eventData);
                        }
                        return;
                    }
                    else if (eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == ITEMTYPE.HELMET ||
                             eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == ITEMTYPE.CHEST ||
                             eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == ITEMTYPE.WEAPON)
                    {
                        if (localStoredItem != eventData.pointerDrag.GetComponent<Item_UI>().item)
                        {
                            localStoredItem = eventData.pointerDrag.GetComponent<Item_UI>().item;
                        }
                        EquipItem(eventData.pointerDrag.gameObject);
                    }
                    base.OnDrop(eventData);
                    return;
                }
            }
        }
        else
        {

        }
    }
    public void RemoveItem()
    {
        equipment.Unequip(index);
        equipment.RemoveItemGameObject(index);
        localStoredItem = null;
    }

    public void EquipItem(GameObject go)
    {
        if (equipment == null)
            equipment = Stats_Player.instance.gameObject.GetComponent<Equipment>();

        equipment.Equip(localStoredItem, index);
        equipment.EquipItemGameObject(go, index);
    }
}