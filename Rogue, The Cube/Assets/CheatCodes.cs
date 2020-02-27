﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{
    public GameObject[] STR_ITEMS;
    public GameObject[] AGI_ITEMS;
    public GameObject[] INT_ITEMS;

    public GameObject[] INV_ITEM;

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            foreach (GameObject str_item in STR_ITEMS)
            {
                str_item.GetComponentInChildren<Item_Click>().MoveItem("InventorySlot");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            foreach (GameObject agi_items in AGI_ITEMS)
            {
                agi_items.GetComponentInChildren<Item_Click>().MoveItem("InventorySlot");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            foreach (GameObject int_items in INT_ITEMS)
            {
                int_items.GetComponentInChildren<Item_Click>().MoveItem("InventorySlot");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            foreach (GameObject inv_item in INV_ITEM)
            {
                inv_item.GetComponentInChildren<Item_Click>().MoveItem("LootSlot");
            }
        }
    }
}