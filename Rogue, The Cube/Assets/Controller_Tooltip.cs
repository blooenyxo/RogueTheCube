using UnityEngine;
using UnityEngine.EventSystems;

public class Controller_Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float xPositionLeftSide = -130f;
    private float xPositionRightSide = 130f;

    private float yPositionDownSide = 75f;
    private float yPositionTopSide = -55f;

    public RectTransform rectTransform;

    public void OnPointerEnter(PointerEventData eventData)
    {
        RespositionTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RespositionTooltip();
    }

    void RespositionTooltip()
    {
        if (this.transform.parent.position.x > Screen.width / 2)
        {
            rectTransform.localPosition = new Vector3(xPositionLeftSide, rectTransform.localPosition.y, rectTransform.localPosition.z);
        }
        else if (this.transform.parent.position.x < Screen.width / 2)
        {
            rectTransform.localPosition = new Vector3(xPositionRightSide, rectTransform.localPosition.y, rectTransform.localPosition.z);
        }

        if (this.transform.parent.position.y > Screen.height / 2)
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, yPositionTopSide, rectTransform.localPosition.z);
        }
        else if (this.transform.parent.position.y < Screen.height / 2)
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, yPositionDownSide, rectTransform.localPosition.z);
        }
    }
}
