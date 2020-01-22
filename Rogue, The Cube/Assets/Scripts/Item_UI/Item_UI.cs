using UnityEngine;
using UnityEngine.UI;

public class Item_UI : MonoBehaviour
{
    public Item item;
    public Image backgroundImage;
    public Image itemImage;

    void Start()
    {
        UpdateItemVisuals();
    }

    public void UpdateItemVisuals()
    {
        SetBackgroundColor();
        SetItemImage();
    }

    private void SetBackgroundColor()
    {
        switch (item.ITEM_CLASS)
        {
            case ITEMCLASS.AGILITY:
                backgroundImage.color = Color.green;
                break;
            case ITEMCLASS.STRENGHT:
                backgroundImage.color = Color.red;
                break;
            case ITEMCLASS.INTELIGENCE:
                backgroundImage.color = Color.blue;
                break;
        }
    }

    private void SetItemImage()
    {
        if (item.sprite != null)
        {
            itemImage.sprite = item.sprite;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = backgroundImage.color;
        }
    }
}