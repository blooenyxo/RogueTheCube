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
    GameObject weapon;

    private void Start()
    {
        equipment = Equipment.instance;
        equipment.onEquipmentChange += UpdateVisuals;
    }

    private void UpdateVisuals(Item newItem, Item oldItem)
    {
        if (newItem == null)
        {
            if (oldItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                Destroy(weapon);
            }
        }
        else
        {
            if (newItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                weapon = Instantiate(newItem.VISUAL_WEAPON, rightHandPoint.transform.position, rightHandPoint.transform.rotation);
                weapon.transform.SetParent(rightHandPoint.transform);
            }
        }
    }
}
