using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour, IDropHandler
{
    public ITEMTYPE[] acceptedItemType;

    public virtual void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            foreach (ITEMTYPE type in acceptedItemType)
            {
                if (eventData.pointerDrag.GetComponent<Item_UI>().item.ITEM_TYPE == type)
                {
                    eventData.pointerDrag.GetComponent<Item_Drag>().parent = this.transform;
                }
            }
        }
        else
        {
            return;
        }
    }
}