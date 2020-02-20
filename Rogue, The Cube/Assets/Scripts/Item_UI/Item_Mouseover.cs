using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Mouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltip;

    private void Start()
    {
        tooltip.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Item_UI>().item.ITEM_TYPE != ITEMTYPE.GOLD)
            tooltip.SetActive(true);

        if (this.transform.parent.CompareTag("Shop") || this.transform.parent.CompareTag("EquipmentSlot"))
        {
            GetComponent<Item_Drag>().enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.GetComponent<Item_UI>().item.ITEM_TYPE != ITEMTYPE.GOLD)
            tooltip.SetActive(false);


        GetComponent<Item_Drag>().enabled = true;
    }
}
