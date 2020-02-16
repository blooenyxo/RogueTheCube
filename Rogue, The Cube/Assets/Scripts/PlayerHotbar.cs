using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbar : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject ButtonQ;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PressButton(Button1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PressButton(Button2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PressButton(Button3);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PressButton(ButtonQ);
        }

        if (GameObject.Find("Player"))
        {
            GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    private void PressButton(GameObject hotbarButton)
    {
        if (hotbarButton.transform.childCount > 0)
        {
            hotbarButton.GetComponentInChildren<Item_Click>().MoveItem(hotbarButton.tag);
        }
    }
}
