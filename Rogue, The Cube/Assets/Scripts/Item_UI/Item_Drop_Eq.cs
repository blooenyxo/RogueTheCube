using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class Item_Drop_Eq : Item_Drop {

    Image image;
    Equipment equipment;
    public Item localStoredItem;
    public int index;

    private void Start () {
        equipment = Equipment.instance;
    }

    public override void OnDrop (PointerEventData eventData) {
        base.OnDrop (eventData);
        if (localStoredItem != eventData.pointerDrag.GetComponent<Item_UI> ().item) {
            localStoredItem = eventData.pointerDrag.GetComponent<Item_UI> ().item;
        }
        EquipItem ();
    }

    public void RemoveItem () {
        equipment.Unequip (index);
    }

    public void EquipItem () {
        equipment.Equip (localStoredItem, index);
    }
}