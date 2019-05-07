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
    GameObject equipmentBackground;

    void Start()
    {
        equipmentBackground = GameObject.Find("EquipmentBackground");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = this.transform.parent;
        _currentParent = this.transform.parent;



        if (transform.parent.GetComponent<Item_Drop_Eq>())
        {
            transform.parent.GetComponent<Item_Drop_Eq>().RemoveItem();
        }

        this.transform.SetParent(this.transform.parent.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    /// <summary>
    /// in this method the end of the drag sequence is being processed. the only imnportant thing is the comparisson between the two stored parent transforms
    /// the _currentparent is only set OnBeginDrag, while the parent is also set in the Item_Drop script, while hovering over a new 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (_currentParent == parent)
        {
            if (parent.GetComponent<Item_Drop_Eq>())
                parent.GetComponent<Item_Drop_Eq>().EquipItem();
        }

        //ClearDropZones();

        this.transform.SetParent(parent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    /// <summary>
    /// the idea here is that we set all the slots, except the one we can still equip to, to inactive
    /// </summary>
    /// <param name="item"></param>
    private void DropZones(Item item)
    {
        if (equipmentBackground == null)
            equipmentBackground = GameObject.Find("EquipmentBackground");

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
        ClearDropZones();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DropZones(GetComponent<Item_UI>().item);
    }
}