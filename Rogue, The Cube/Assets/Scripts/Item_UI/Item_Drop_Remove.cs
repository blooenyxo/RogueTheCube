using UnityEngine.EventSystems;

public class Item_Drop_Remove : Item_Drop
{
    public override void OnDrop(PointerEventData eventData)
    {
        base.OnDrop(eventData);
        // this is where the question will be asked... destroy item, yes or no
        Destroy(eventData.pointerDrag.gameObject);
    }
}
