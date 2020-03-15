using System.Collections.Generic;
using UnityEngine;

public class LootDrop_Controller : MonoBehaviour
{
    public int maxNrOfLowTierItems;
    public int maxNrOfMediumTierItems;
    public int maxNrOfHighTierItems;
    public int maxNrOfConsumablesItems;
    public int maxNrOfspecialItems;

    public int minConsumablesStacks;
    public int maxConsumablesStacks;

    public int minGoldStacks;
    public int maxGoldStacks;

    public int chanceToDropLootbox;

    public List<Item> Drop()
    {
        List<Item> allItems = new List<Item>();
        allItems.AddRange(ItemDistriburion.instance.PickItems(maxNrOfLowTierItems, ItemDistriburion.instance.LowTierItems));
        allItems.AddRange(ItemDistriburion.instance.PickItems(maxNrOfMediumTierItems, ItemDistriburion.instance.MediumTierItems));
        allItems.AddRange(ItemDistriburion.instance.PickItems(maxNrOfHighTierItems, ItemDistriburion.instance.HighTierItems));
        allItems.AddRange(ItemDistriburion.instance.PickItems(maxNrOfConsumablesItems, ItemDistriburion.instance.Consumables));
        allItems.AddRange(ItemDistriburion.instance.PickItems(maxNrOfspecialItems, ItemDistriburion.instance.SpecialItem));

        return allItems;
    }

    public void SetLootBoxAndItems()
    {
        //Debug.Log("setlootboxanditems");

        if (Random.Range(0, 101) <= chanceToDropLootbox)
        {

            GameObject lb = Instantiate(GetComponent<Equipment_Visual_Enemy>().lootBox, this.transform.position, this.transform.rotation);

            int random = Random.Range(0, Drop().Count);

            List<Item> tempList = new List<Item>();

            List<int> tempStacks = new List<int>();

            for (int i = 0; i < random; i++)
            {
                int random_ = Random.Range(0, Drop().Count);
                tempList.Add(Drop()[random_]);
            }

            // list init
            for (int j = 0; j < tempList.Count; j++)
            {
                tempStacks.Add(0);
            }


            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j].ITEM_TYPE == ITEMTYPE.ARROW || tempList[j].ITEM_TYPE == ITEMTYPE.CONSUMABLE)
                {
                    tempStacks[j] = Random.Range(minConsumablesStacks, maxConsumablesStacks + 1);
                }
                else if (tempList[j].ITEM_TYPE == ITEMTYPE.GOLD)
                {
                    tempStacks[j] = Random.Range(minGoldStacks, maxGoldStacks + 1);
                }
                else
                {
                    tempStacks[j] = 1;
                }
            }

            // add a gold item from a list of gold items, or set a random range here !!! currently every mob drops the 500 goldpile item;
            //lb.GetComponent<LootBox_Controller>().items.Add(GoldToDrop);
            lb.GetComponent<LootBox_Controller>().stacks = tempStacks;
            lb.GetComponent<LootBox_Controller>().items.AddRange(tempList);
        }
    }
}