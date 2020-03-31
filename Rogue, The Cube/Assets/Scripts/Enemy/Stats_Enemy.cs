using UnityEngine;
using UnityEngine.AI;

public class Stats_Enemy : Stats
{
    public delegate void OnEnmeyHit(int cHealth, int mHealth, GameObject enemyGameObject);
    public OnEnmeyHit onEnemyHealthChange;

    public delegate void OnEnemyDeath(GameObject deadEnemy);
    public OnEnemyDeath onEnemyDeath;

    public void SetValues(Enemy enemy)
    {
        STRENGHT.AddModifier(enemy.STRENGHT);
        INTELIGENCE.AddModifier(enemy.INTELIGENCE);
        AGILITY.AddModifier(enemy.AGILITY);
        HITPOINTS.AddModifier(enemy.HITPOINTS);
        MANAPOINTS.AddModifier(enemy.MANAPOINTS);
        MOVESPEED.AddModifier(enemy.MOVESPEED);
        STAMINA.AddModifier(enemy.STAMINA);
    }

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
        onEnemyHealthChange?.Invoke(CurrentHealth, HITPOINTS.GetValue(), this.gameObject);
    }

    public override void Heal(int value)
    {
        base.Heal(value);
        onEnemyHealthChange?.Invoke(CurrentHealth, HITPOINTS.GetValue(), this.gameObject);
        GameObject tmp = Instantiate(GetComponent<Equipment_Visual>().healingEffect, this.transform.position, this.transform.rotation);
        tmp.transform.SetParent(this.transform);
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
        GetComponent<LootDrop_Controller>().SetLootBoxAndItems();
        onEnemyDeath?.Invoke(this.gameObject);
        base.Die();
    }
}