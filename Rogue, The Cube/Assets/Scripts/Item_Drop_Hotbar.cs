using UnityEngine.EventSystems;

public class Item_Drop_Hotbar : Item_Drop
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

                    // also, remove the gameobject
                    Destroy(eventData.pointerDrag.gameObject);
                }
            }
        }
    }
}