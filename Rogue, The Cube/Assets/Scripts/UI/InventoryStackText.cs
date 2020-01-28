﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryStackText : MonoBehaviour
{
    public Text[] stackText;
    public Item_Drop_Inv[] invSlots;

    private void Start()
    {
        AdjustStackValues();
    }

    private void Update()
    {
        if (GetComponentInParent<CanvasGroup>().alpha == 1f)
            AdjustStackValues();
    }

    void AdjustStackValues()
    {
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (invSlots[i].gameObject.transform.childCount > 1f)
            {
                stackText[i].gameObject.GetComponent<CanvasGroup>().alpha = 1f;
                stackText[i].text = invSlots[i].gameObject.transform.childCount.ToString();

                if (invSlots[i].GetComponentInChildren<Item_UI>())
                {
                    if (invSlots[i].GetComponentInChildren<Item_UI>().item.ITEM_CLASS == ITEMCLASS.AGILITY)
                        stackText[i].color = Color.black;
                    if (invSlots[i].GetComponentInChildren<Item_UI>().item.ITEM_CLASS == ITEMCLASS.INTELIGENCE)
                        stackText[i].color = Color.black;
                    if (invSlots[i].GetComponentInChildren<Item_UI>().item.ITEM_CLASS == ITEMCLASS.NONE)
                        stackText[i].color = Color.black;
                    if (invSlots[i].GetComponentInChildren<Item_UI>().item.ITEM_CLASS == ITEMCLASS.STRENGHT)
                        stackText[i].color = Color.black;
                }
            }
            else
            {
                stackText[i].gameObject.GetComponent<CanvasGroup>().alpha = 0f;
            }
        }
    }
}