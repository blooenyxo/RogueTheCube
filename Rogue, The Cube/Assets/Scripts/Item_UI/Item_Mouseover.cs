using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Mouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltip;
    public CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = tooltip.GetComponent<CanvasGroup>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.GetComponent<Item_UI>().item.ITEM_TYPE != ITEMTYPE.GOLD)
            _canvasGroup.alpha = 1f;

        if (this.transform.parent.CompareTag("Shop") || this.transform.parent.CompareTag("EquipmentSlot"))
        {
            GetComponent<Item_Drag>().enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.GetComponent<Item_UI>().item.ITEM_TYPE != ITEMTYPE.GOLD)
            _canvasGroup.alpha = 0f;


        GetComponent<Item_Drag>().enabled = true;
    }
}
