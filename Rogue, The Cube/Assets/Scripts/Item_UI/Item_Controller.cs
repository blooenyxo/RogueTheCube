using System.Collections.Generic;
using UnityEngine;

public class Item_Controller : MonoBehaviour
{
    public int maxNrOfLowTierItems;
    public int maxNrOfMediumTierItems;
    public int maxNrOfHighTierItems;
    public int maxNrOfConsumablesItems;
    public int maxNrOfspecialItems;
    public Item GoldToDrop;

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

            for (int i = 0; i < random; i++)
            {
                int random_ = Random.Range(0, Drop().Count);
                tempList.Add(Drop()[random_]);
            }

            // add a gold item from a list of gold items, or set a random range here !!! currently every mob drops the 500 goldpile item;
            lb.GetComponent<LootBox_Controller>().items.Add(GoldToDrop);
            lb.GetComponent<LootBox_Controller>().items.AddRange(tempList);
        }
    }
}