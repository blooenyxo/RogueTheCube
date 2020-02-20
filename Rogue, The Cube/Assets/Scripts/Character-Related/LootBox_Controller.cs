using System.Collections.Generic;
using UnityEngine;

public class LootBox_Controller : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Animator animator;

    private void Start()
    {
        Destroy(gameObject, 120f);
    }
    private void Update()
    {
        if (items.Count <= 0)
        {
            Destroy(gameObject, .5f);
        }
    }

    public void RemoveItemFromList(Item item)
    {
        items.Remove(item);
    }

    public void LootboxOpen()
    {
        animator.SetTrigger("lootboxopen");
    }
}