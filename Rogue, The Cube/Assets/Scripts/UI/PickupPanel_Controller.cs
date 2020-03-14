using UnityEngine;

public class PickupPanel_Controller : MonoBehaviour
{
    public Transform[] slots;
    public GameObject UI_Item;
    public Controller_Input _cp;
    public bool loadedInPanel = false;

    public void SetupPanel(GameObject lootBox)
    {
        foreach (Item item in lootBox.GetComponent<LootBox_Controller>().items)
        {
            CreateItem(item, lootBox);
        }
        loadedInPanel = true;
    }

    void CreateItem(Item item, GameObject lootBox)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].childCount == 0)
            {
                GameObject _ui_item = Instantiate(UI_Item, slots[i].transform.position, slots[i].transform.rotation, slots[i]);
                _ui_item.GetComponent<Item_UI>().item = item;

                if (item.ITEM_TYPE == ITEMTYPE.ARROW || item.ITEM_TYPE == ITEMTYPE.CONSUMABLE || item.ITEM_TYPE == ITEMTYPE.GOLD)
                    _ui_item.GetComponent<Item_UI>().stacks = lootBox.GetComponent<LootBox_Controller>().stacks[i];

                _ui_item.GetComponent<Item_UI>().UpdateItemVisuals();
                return;
            }
        }
    }

    public void RemoveItemFromPanel()
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 1)
            {
                Destroy(slot.GetChild(0).gameObject);
            }
        }

        loadedInPanel = false;
    }
}