using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Visual : MonoBehaviour
{
    public GameObject rightHandPoint;
    public GameObject leftHandPoint;
    public GameObject headPoint;

    Equipment equipment;

    private void Start()
    {
        equipment = Equipment.instance;

        equipment.onEquipmentChange += UpdateVisuals;
    }

    private void UpdateVisuals(Item newItem, Item oldItem)
    {
        if (newItem.ITEM_TYPE == ITEMTYPE.WEAPON)
        {
            Instantiate(newItem.VISUAL_WEAPON, rightHandPoint.transform.position, rightHandPoint.transform.rotation);
        }
    }
}
