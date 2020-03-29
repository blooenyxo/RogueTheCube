using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Drop_Inv : Item_Drop
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        if (transform.childCount > 0)
        {
            if (eventData.pointerDrag.GetComponent<Item_UI>().item == transform.GetComponentInChildren<Item_UI>().item)
            {
                if (transform.GetComponentInChildren<Item_UI>().item.stackable == true)
                {
                    transform.GetComponentInChildren<Item_UI>().stacks += eventData.pointerDrag.GetComponent<Item_UI>().stacks;
                    transform.GetComponentInChildren<Item_UI>().AdjustStackText();

                    // the next line is for clearing the lootbox content after item was removed
                    if (eventData.pointerDrag.GetComponent<Item_Drag>()._currentParent.CompareTag("LootSlot"))
                        GameObject.Find("Player").GetComponent<Controller_Input>().NearbyInteraction().GetComponent<LootBox_Controller>().RemoveItemFromList(eventData.pointerDrag.GetComponent<Item_UI>().item);

                    // also, remove the gameobject
                    Destroy(eventData.pointerDrag.gameObject);
                }
            }
        }
    }
}