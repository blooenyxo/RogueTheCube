using System.Collections.Generic;
using UnityEngine;

public class NPCPanel_Controller : MonoBehaviour
{
    public GameObject ui_item;
    public GameObject ShopPanel;
    public GameObject QuestPanel;
    public Transform[] shopSlots;

    public void Setup(TypeOfNPC _typeOfNPC)
    {
        switch (_typeOfNPC)
        {
            case TypeOfNPC.QuestGiver:
                QuestPanel.GetComponent<CanvasGroup>().alpha = 1;
                QuestPanel.GetComponent<CanvasGroup>().interactable = true;
                QuestPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                break;
            case TypeOfNPC.Shop:
                ShopPanel.GetComponent<CanvasGroup>().alpha = 1;
                ShopPanel.GetComponent<CanvasGroup>().interactable = true;
                ShopPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;
                SetupShop(GameObject.Find("Player").GetComponent<Controller_Input>().NearbyInteraction().GetComponent<NPC_Controller>().items, GameObject.Find("Player").GetComponent<Controller_Input>().NearbyInteraction().GetComponent<NPC_Controller>().stacks);
                break;
            default:
                break;
        }
    }

    public void ClearNPCPanel()
    {
        ShopPanel.GetComponent<CanvasGroup>().alpha = 0;
        ShopPanel.GetComponent<CanvasGroup>().interactable = false;
        ShopPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ClearShop();

        QuestPanel.GetComponent<CanvasGroup>().alpha = 0;
        QuestPanel.GetComponent<CanvasGroup>().interactable = false;
        QuestPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void SetupShop(List<Item> _itemsToSell, int[] _stacks)
    {
        for (int i = 0; i < _itemsToSell.Count; i++)
        {
            GameObject _ui_item = Instantiate(ui_item, transform.position, transform.rotation, transform);
            _ui_item.GetComponent<Item_UI>().item = _itemsToSell[i];

            if (_stacks.Length > 0)
                _ui_item.GetComponent<Item_UI>().stacks = _stacks[i];

            for (int j = shopSlots.Length - 1; j >= 0; j--)
            {
                if (shopSlots[j].childCount == 0)
                {
                    _ui_item.transform.SetParent(shopSlots[j]);
                }
            }

        }
    }

    public void ClearShop()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (shopSlots[i].childCount > 0)
            {
                Destroy(shopSlots[i].transform.GetChild(0).gameObject);
            }
        }
    }

}