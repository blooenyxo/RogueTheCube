using UnityEngine;
using UnityEngine.UI;

public class Item_UI : MonoBehaviour
{
    public Item item;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();

        SetImageColor();
    }

    void SetImageColor()
    {
        switch (item.ITEM_CLASS)
        {
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