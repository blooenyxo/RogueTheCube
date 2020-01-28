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
                eventData.pointerDrag.GetComponent<Item_Drag>().parent = this.transform;
            }
        }
    }
}