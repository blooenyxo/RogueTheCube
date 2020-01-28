using System.Collections.Generic;
using UnityEngine;

public class ItemDistriburion : MonoBehaviour
{
    #region Singelton
    public static ItemDistriburion instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one Inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    public List<Item> LowTierItems = new List<Item>();
    public List<Item> MediumTierItems = new List<Item>();
    public List<Item> HighTierItems = new List<Item>();
    public List<Item> Consumables = new List<Item>();
    public List<Item> SpecialItem = new List<Item>();

    public List<Item> PickItems(int numberOfItems, List<Item> itemTier)
    {
        List<Item> tempList = new List<Item>();
        for (int i = 0; i < numberOfItems; i++)
        {
            int random = Random.Range(0, itemTier.Count);
            tempList.Add(itemTier[random]);
        }
        return tempList;
    }
}
