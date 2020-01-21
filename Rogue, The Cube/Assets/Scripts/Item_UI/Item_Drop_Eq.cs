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
        equipment = Equipment.instance;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        // this logic handles what happens if after picking an item off a equipmentslot you decide to place it right back down.
        if (transform.childCount == 0)
        {
            foreach (ITEMTYPE type in acceptedItemType)
            {
                if (eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == type)
                {
                    if (localStoredItem != eventData.pointerDrag.GetComponent<Item_UI>().item)
                    {
                        localStoredItem = eventData.pointerDrag.GetComponent<Item_UI>().item;
                    }
                    EquipItem();
                }
            }
        }
    }
    public void RemoveItem()
    {
        equipment.Unequip(index);
    }

    public void EquipItem()
    {
        equipment.Equip(localStoredItem, index);
    }
}