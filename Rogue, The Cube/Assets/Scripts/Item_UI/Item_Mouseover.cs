using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Mouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltip;
    private CanvasGroup _canvasGroup;

    private bool mouseOver = false;
    private float fadeSpeed = 100f;

    private void Start()
    {
        _canvasGroup = tooltip.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (mouseOver)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;

        if (this.transform.parent.CompareTag("Shop") /*|| this.transform.parent.CompareTag("EquipmentSlot")*/)
        {
            GetComponent<Item_Drag>().enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;

        GetComponent<Item_Drag>().enabled = true;
    }

    void FadeIn()
    {
        if (_canvasGroup.alpha < 1f)
            _canvasGroup.alpha += fadeSpeed * Time.deltaTime;
    }

    void FadeOut()
    {
        if (_canvasGroup.alpha > 0f)
            _canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
    }
}