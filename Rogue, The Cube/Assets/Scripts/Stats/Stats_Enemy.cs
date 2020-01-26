using System.Collections.Generic;
using UnityEngine;

public class Stats_Enemy : Stats
{
    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
    }

    public override void Die()
    {
        base.Die();
        SetLootBoxAndItems();
    }

    private void SetLootBoxAndItems()
    {
        GameObject lb = Instantiate(GetComponent<Equipment_Visual>().lootBox, this.transform.position, this.transform.rotation);

        int random = Random.Range(0, GetComponent<ItemDrop>().Drop().Count);
        List<Item> tempList = new List<Item>();

        for (int i = 0; i < random; i++)
        {
            int random_ = Random.Range(0, GetComponent<ItemDrop>().Drop().Count);
            tempList.Add(GetComponent<ItemDrop>().Drop()[random_]);
        }

        lb.GetComponent<LootBox_Controller>().items.AddRange(tempList);
    }
}