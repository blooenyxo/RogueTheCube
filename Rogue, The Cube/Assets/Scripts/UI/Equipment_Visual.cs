﻿using UnityEngine;

public abstract class Equipment_Visual : MonoBehaviour
{
    [Header("Equipment Slots")]
    public GameObject rightHandPoint;
    public GameObject leftHandPoint;
    public GameObject headPoint;

    [Header("On Death")]
    public GameObject deathBody;
    public GameObject aliveBody;

    [Header("Hit Marker")]
    public GameObject hitMarker;

    GameObject weapon;
    GameObject offhand;

    public virtual void Start() { }

    public virtual void UpdateVisuals(Item newItem, Item oldItem)
    {
        if (newItem == null)
        {
            if (oldItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                Destroy(weapon);
            }
            else if (oldItem.ITEM_TYPE == ITEMTYPE.OFFHAND)
            {
                Destroy(offhand);
            }
        }
        else
        {
            if (newItem == oldItem)
            {
                return;
            }

            if (newItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                weapon = Instantiate(newItem.VISUAL_MODEL, rightHandPoint.transform.position, rightHandPoint.transform.rotation);
                weapon.transform.SetParent(rightHandPoint.transform);
            }
            else if (newItem.ITEM_TYPE == ITEMTYPE.OFFHAND)
            {
                // here add what to do when you add in the offhand slot a different Arrow type
                offhand = Instantiate(newItem.VISUAL_MODEL, leftHandPoint.transform.position, rightHandPoint.transform.rotation);
                offhand.transform.SetParent(leftHandPoint.transform);
            }
        }
    }

    public virtual void HitMarker(Vector3 where)
    {
        GameObject hm = Instantiate(hitMarker, where, Quaternion.identity); // dont parent the cubes to this gameobject
    }
}