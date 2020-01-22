using System.Collections.Generic;
using UnityEngine;

public class LootBox_Controller : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    private void Start()
    {
        Destroy(gameObject, 30f);
    }

    private void Update()
    {
        if (items.Count <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void RemoveItemFromArray(Item item)
    {
        items.Remove(item);
    }
}
