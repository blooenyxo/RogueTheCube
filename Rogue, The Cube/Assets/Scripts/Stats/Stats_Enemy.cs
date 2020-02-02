using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats_Enemy : Stats
{
    public delegate void OnEnmeyHit(int cHealth, int mHealth, string enemyName);
    public OnEnmeyHit onEnemyHit;

    public override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        // works for some reason. i dont realy know. multiple delegate maybe. 
        // i think they all get triggered so we have to check if its null or not. the syntax does eaxctly that ( if (onEnemyHit != null) )
        // still a problem with new spawned enemies, the listener is not subscribed anymore
        onEnemyHit?.Invoke(CurrentHealth, HITPOINTS.GetValue(), this.gameObject.tag);
    }

    public override void SetMovespeed(int value)
    {
        base.SetMovespeed(value);
        GetComponent<NavMeshAgent>().speed = MOVESPEED.GetValue();
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    public override void ResetMovespeed(int value)
    {
        base.ResetMovespeed(value);
        GetComponent<NavMeshAgent>().speed = MOVESPEED.GetValue();
        GetComponent<NavMeshAgent>().isStopped = false;
    }

    public override void Die()
    {
        base.Die();
        SetLootBoxAndItems();
    }

    private void SetLootBoxAndItems()
    {
        GameObject lb = Instantiate(GetComponent<Equipment_Visual_Enemy>().lootBox, this.transform.position, this.transform.rotation);

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