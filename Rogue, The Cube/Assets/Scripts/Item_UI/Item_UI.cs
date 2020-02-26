using UnityEngine;
using UnityEngine.UI;

public class Item_UI : MonoBehaviour
{
    public Item item;
    public Image backgroundImage;
    public Image itemImage;

    public int stacks;
    public Text stackText;
    public CanvasGroup cg;

    public Text ToolTip_Title;
    public Text ToolTip_FirstStat;
    public Text ToolTip_SecondStat;
    public Text ToolTip_ThirdStat;
    public Text ToolTip_ForthStat;
    public Text ToolTip_FifthStat;
    public Text ToolTip_Description;
    public Text ToolTip_Gold;

    public GameObject toolTipBorder;

    void Start()
    {
        UpdateItemVisuals();
        AdjustStackText();
        ResetToolTipStatsText();
        SetToolTip();
    }

    void SetToolTip()
    {
        ToolTip_Title.text = item.ITEMNAME;

        if (item.ITEM_TYPE == ITEMTYPE.CHEST || item.ITEM_TYPE == ITEMTYPE.HELMET)
        {
            BasicStats();
        }

        if (item.ITEM_TYPE == ITEMTYPE.CONSUMABLE)
        {
            if (item.Health > 0f && item.Mana == 0f)
            {
                ToolTip_FirstStat.gameObject.SetActive(true);
                ToolTip_FirstStat.text = " + " + item.Health.ToString() + " Health";
            }
            else if (item.Mana > 0f && item.Health == 0f)
            {
                ToolTip_FirstStat.gameObject.SetActive(true);
                ToolTip_FirstStat.text = " + " + item.Mana.ToString() + " Mana";
            }
            else if (item.Health > 0f && item.Mana > 0f)
            {
                ToolTip_FirstStat.gameObject.SetActive(true);
                ToolTip_FirstStat.text = " + " + item.Health.ToString() + " Health";
                ToolTip_SecondStat.gameObject.SetActive(true);
                ToolTip_SecondStat.text = " + " + item.Mana.ToString() + " Mana";
            }
        }

        if (item.ITEM_TYPE == ITEMTYPE.WEAPON || item.ITEM_TYPE == ITEMTYPE.OFFHAND || item.ITEM_TYPE == ITEMTYPE.ARROW)
        {
            BasicStats();

            if (item.MAXDMG != 0)
            {
                ToolTip_ForthStat.gameObject.SetActive(true);
                ToolTip_ForthStat.text = item.MINDMG.ToString() + " - " + item.MAXDMG.ToString() + " DMG";
            }

            if (item.MAXMAGIC != 0)
            {
                ToolTip_FifthStat.gameObject.SetActive(true);
                ToolTip_FifthStat.text = item.MINMAGIC.ToString() + " - " + item.MAXMAGIC.ToString() + " MAGIC";
            }
        }

        if (item.DESCRIPTION.Length > 0)
        {
            ToolTip_Description.gameObject.SetActive(true);
            ToolTip_Description.text = item.DESCRIPTION;
        }

        ToolTip_Gold.gameObject.SetActive(true);
        if (item.stackable)
            AdjustStackText();
        else
            ToolTip_Gold.text = item.Gold.ToString() + " Gold";

        SetBackgroundColor(toolTipBorder.GetComponent<Image>());
    }

    void ResetToolTipStatsText()
    {
        ToolTip_FirstStat.gameObject.SetActive(false);
        ToolTip_SecondStat.gameObject.SetActive(false);
        ToolTip_ThirdStat.gameObject.SetActive(false);
        ToolTip_ForthStat.gameObject.SetActive(false);
        ToolTip_FifthStat.gameObject.SetActive(false);
        ToolTip_Description.gameObject.SetActive(false);
        ToolTip_Gold.gameObject.SetActive(false);
    }

    void BasicStats()
    {
        if (item.STRENGHT == 0f)
            ToolTip_FirstStat.gameObject.SetActive(false);
        else
        {
            ToolTip_FirstStat.gameObject.SetActive(true);
            ToolTip_FirstStat.text = " + " + item.STRENGHT.ToString() + " STR";
        }

        if (item.INTELIGENCE == 0f)
            ToolTip_SecondStat.gameObject.SetActive(false);
        else
        {
            ToolTip_SecondStat.gameObject.SetActive(true);
            ToolTip_SecondStat.text = " + " + item.INTELIGENCE.ToString() + " INT";
        }

        if (item.AGILITY == 0f)
            ToolTip_ThirdStat.gameObject.SetActive(false);
        else
        {
            ToolTip_ThirdStat.gameObject.SetActive(true);
            ToolTip_ThirdStat.text = " + " + item.AGILITY.ToString() + " AGI";

        }
    }

    public void AdjustStackText()
    {
        if (item.stackable)
        {
            if (stacks > 1)
            {
                cg.alpha = 1f;
                stackText.text = stacks.ToString();
                ToolTip_Gold.text = item.Gold.ToString() + " Gold" + " - " + (item.Gold * stacks).ToString() + " Gold / stack";
            }
            else
            {
                cg.alpha = 0f;
            }

        }
        else
        {
            cg.alpha = 0f;
        }
    }

    public void UpdateItemVisuals()
    {
        SetBackgroundColor(backgroundImage);
        SetItemImage();
    }

    private void SetBackgroundColor(Image img)
    {
        switch (item.ITEM_CLASS)
        {
            case ITEMCLASS.AGILITY:
                img.color = Color.green;
                break;
            case ITEMCLASS.STRENGHT:
                img.color = Color.red;
                break;
            case ITEMCLASS.INTELIGENCE:
                img.color = Color.blue;
                break;
            case ITEMCLASS.NONE:
                img.color = Color.white;
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

    private void KeepTooltipOnScreen()
    {

    }
}