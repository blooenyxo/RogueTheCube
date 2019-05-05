using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_UI : MonoBehaviour {
    public Item item;

    Image image;

    void Start () {
        image = GetComponent<Image> ();

        switch (item.ITEM_CLASS) {
            case ITEMCLASS.AGILITY:
                image.color = Color.green;
                break;
            case ITEMCLASS.STRENGHT:
                image.color = Color.red;
                break;
            case ITEMCLASS.INTELIGENCE:
                image.color = Color.blue;
                break;
        }
    }

}