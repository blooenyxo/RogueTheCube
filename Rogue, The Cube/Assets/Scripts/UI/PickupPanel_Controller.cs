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
            CreateItem(item);
        }
        loadedInPanel = true;
    }

    void CreateItem(Item item)
    {
        foreach (Transform slot in slots)
        {
            if (slot.childCount == 0)
            {
                GameObject _ui_item = Instantiate(UI_Item, slot.transform.position, slot.transform.rotation, slot);
                _ui_item.GetComponent<Item_UI>().item = item;

                if (item.ITEM_TYPE == ITEMTYPE.ARROW || item.ITEM_TYPE == ITEMTYPE.CONSUMABLE)
                    _ui_item.GetComponent<Item_UI>().stacks = Mathf.RoundToInt(Random.Range(0, 20));

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