﻿using System.Collections.Generic;
using UnityEngine;

public class LootBox_Controller : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<int> stacks = new List<int>();
    private Animator animator;
    public bool despawn = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (despawn)
            Destroy(gameObject, 120f);
    }
    private void Update()
    {
        if (items.Count <= 0 && despawn)
        {
            Destroy(gameObject, .5f);
        }
    }

    public void RemoveItemFromList(Item item)
    {
        stacks.RemoveAt(items.IndexOf(item));
        items.Remove(item);
    }

    public void LootboxOpen()
    {
        animator.SetTrigger("lootboxopen");
    }
}