using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public int maxNrOfLowTierItems;
    public int maxNrOfMediumTierItems;
    public int maxNrOfHighTierItems;
    public int maxNrOfConsumablesItems;
    public int maxNrOfspecialItems;

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
}
