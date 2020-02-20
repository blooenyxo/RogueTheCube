using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Drop_Shop : MonoBehaviour, IDropHandler
{
    public Transform[] buybackslots = new Transform[4];

    public void OnDrop(PointerEventData eventData)
    {
        SellItem(eventData.pointerDrag);
    }

    public void SellItem(GameObject itemToSell)
    {
        if (buybackslots[0].childCount >= 1)
        {
            buybackslots[0].GetChild(0).SetParent(buybackslots[1]);
        }
        if (buybackslots[1].childCount > 1)
        {
            buybackslots[1].GetChild(0).SetParent(buybackslots[2]);
        }
        if (buybackslots[2].childCount > 1)
        {
            buybackslots[2].GetChild(0).SetParent(buybackslots[3]);
        }
        if (buybackslots[3].childCount > 1)
        {
            Destroy(buybackslots[3].GetChild(0).gameObject);
        }

        Stats_Player.instance.GainGold(itemToSell.GetComponent<Item_UI>().item.Gold * itemToSell.GetComponent<Item_UI>().stacks);

        // leave both of these here. works with item_click and item_drag this way
        itemToSell.GetComponent<Item_Drag>().parent = buybackslots[0].transform;
        itemToSell.transform.SetParent(buybackslots[0].transform);
    }
}