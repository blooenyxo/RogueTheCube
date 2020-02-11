using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// this script sits on the game object that is being draged around
/// </summary>
public class Item_Drag : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
{


    /// <summary>
    /// two parent references, used for comparisson later in the script
    /// </summary>
    [HideInInspector] public Transform parent = null;
    [HideInInspector] public Transform _currentParent = null;

    /// <summary>
    /// game object reference to the parent of the Item_Drop_Eq holders
    /// </summary>
    private GameObject equipmentBackground;
    private GameObject topCanvas;

    void Start()
    {
        equipmentBackground = GameObject.Find("EquipmentBackground");
        topCanvas = GameObject.FindGameObjectWithTag("TopCanvas");
    }

    /// <summary>
    /// mouse button should be defined here. if this will stick, and we will use a mouse or keyboard for the final game, then 
    /// the difference between left and right clicks should be defined. 
    /// so do that here and in the right click handler.
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            parent = this.transform.parent;
            _currentParent = this.transform.parent;

            if (transform.parent.GetComponent<Item_Drop_Eq>())
            {
                transform.parent.GetComponent<Item_Drop_Eq>().RemoveItem();
            }

            this.transform.SetParent(topCanvas.transform);
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = eventData.position;
        }
    }

    /// <summary>
    /// in this method the end of the drag sequence is being processed. the only imnportant thing is the comparisson between the two stored parent transforms
    /// the _currentparent is only set OnBeginDrag, while the parent is also set in the Item_Drop script, while hovering over a new 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_currentParent == parent)
            {
                if (parent.GetComponent<Item_Drop_Eq>())
                    parent.GetComponent<Item_Drop_Eq>().EquipItem(this.gameObject);
            }
            //ClearDropZones();
            this.transform.SetParent(parent);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    /// <summary>
    /// the idea here is that we set all the slots, except the one we can still equip to, to inactive
    /// </summary>
    /// <param name="item"></param>
    private void DropZones(Item item)
    {
        if (equipmentBackground == null)
            equipmentBackground = GameObject.Find("EquipmentBackground");

        /// i must investigate this one. why not if else.
        if (equipmentBackground == null)
            return;

        switch (item.ITEM_TYPE)
        {
            case ITEMTYPE.HELMET:
                equipmentBackground.transform.GetChild(1).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(1).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(2).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(2).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(3).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(3).GetComponent<Item_Drop_Eq>().enabled = false;
                break;
            case ITEMTYPE.CHEST:
                equipmentBackground.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(0).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(2).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(2).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(3).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(3).GetComponent<Item_Drop_Eq>().enabled = false;
                break;
            case ITEMTYPE.WEAPON:
                equipmentBackground.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(0).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(1).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(1).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(3).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(3).GetComponent<Item_Drop_Eq>().enabled = false;
                break;
            case ITEMTYPE.OFFHAND:
                equipmentBackground.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(0).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(1).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(1).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(2).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(2).GetComponent<Item_Drop_Eq>().enabled = false;
                break;
            case ITEMTYPE.ARROW:
                equipmentBackground.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(0).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(1).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(1).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(2).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(2).GetComponent<Item_Drop_Eq>().enabled = false;
                break;
            case ITEMTYPE.SPELL:
                equipmentBackground.transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(0).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(1).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(1).GetComponent<Item_Drop_Eq>().enabled = false;
                equipmentBackground.transform.GetChild(2).GetComponent<Image>().color = Color.grey;
                equipmentBackground.transform.GetChild(2).GetComponent<Item_Drop_Eq>().enabled = false;
                break;
        }
    }

    /// <summary>
    /// set all the equipment slots back to default. all actvie and white
    /// </summary>
    private void ClearDropZones()
    {
        if (equipmentBackground == null)
            return;

        equipmentBackground.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        equipmentBackground.transform.GetChild(0).GetComponent<Item_Drop_Eq>().enabled = true;
        equipmentBackground.transform.GetChild(1).GetComponent<Image>().color = Color.white;
        equipmentBackground.transform.GetChild(1).GetComponent<Item_Drop_Eq>().enabled = true;
        equipmentBackground.transform.GetChild(2).GetComponent<Image>().color = Color.white;
        equipmentBackground.transform.GetChild(2).GetComponent<Item_Drop_Eq>().enabled = true;
        equipmentBackground.transform.GetChild(3).GetComponent<Image>().color = Color.white;
        equipmentBackground.transform.GetChild(3).GetComponent<Item_Drop_Eq>().enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
            ClearDropZones();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
            DropZones(GetComponent<Item_UI>().item);
    }
}