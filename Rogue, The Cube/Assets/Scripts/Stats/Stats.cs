using UnityEngine;
/// <summary>
/// class for holding and manipulating stats. inherited by StatsPlayer and StatsEnemy
/// the methods are virtual so they can be overrwritten in case of extra functionality on the Player / Enemy side is needed
/// </summary>
public class Stats : MonoBehaviour
{
    public Stat STRENGHT;
    public Stat INTELIGENCE;
    public Stat AGILITY;
    public Stat HITPOINTS;
    public Stat MANAPOINTS;
    public Stat MOVESPEED;
    public Stat ARMOR;
    public Stat MINDMG;
    public Stat MAXDMG;

    public int currentHealth { get; private set; }
    public int currentMana { get; private set; }

    private void Start()
    {
        currentHealth = HITPOINTS.GetValue();
        currentMana = MANAPOINTS.GetValue();
    }

    /// <summary>
    /// Health Stuff
    /// </summary>
    //TODO adjust the importance of armor here
    public virtual void TakeDamage(int value)
    {
        value -= ARMOR.GetValue();
        value = Mathf.Clamp(value, 1, int.MaxValue);
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    /// <summary>
    /// a rudimentary crit chance + crit amount system. work in progress.
    /// </summary>
    /// <returns>int damage, to be used with the targets takedamage() method</returns>
    public virtual int DealDamage()
    {
        int i = UnityEngine.Random.Range(1, 101);
        if (i > CritChance())
        {
            return BaseDamage();
        }
        else
        {
            return Mathf.RoundToInt(BaseDamage() * CritAmount());
        }
    }
    private int BaseDamage()
    {
        return Mathf.CeilToInt(UnityEngine.Random.Range(MINDMG.GetValue(), MAXDMG.GetValue()) + (STRENGHT.GetValue() * .2f));
    }
    private float CritAmount()
    {
        float value = AGILITY.GetValue() * .2f;
        Mathf.Clamp(value, 1.5f, float.MaxValue);
        return value;
    }
    private float CritChance()
    {
        return AGILITY.GetValue() * 1.5f;
    }
    public virtual void Heal(int value)
    {
        currentHealth += value;
        if (currentHealth > HITPOINTS.GetValue())
        {
            currentHealth = HITPOINTS.GetValue();
        }
    }
    /// <summary>
    /// Mana Stuff
    /// </summary>
    public virtual void GainMana(int value)
    {
        currentMana += value;
        if (currentMana > MANAPOINTS.GetValue())
        {
            currentMana = MANAPOINTS.GetValue();
        }
    }

    public virtual bool UseMana(int value)
    {
        currentMana -= value;
        if (currentMana <= 0)
        {
            currentMana = 0;
            return false;
        }
        return true;
    }
    public virtual void Die()
    {
        //TODO there is nothing in the Die() method.
    }

    public virtual void OnEquipmentChange(Item newItem, Item oldItem)
    {
        if (newItem != null)
        {
            STRENGHT.AddModifier(newItem.STRENGHT);
            INTELIGENCE.AddModifier(newItem.INTELIGENCE);
            AGILITY.AddModifier(newItem.AGILITY);
            MOVESPEED.AddModifier(Mathf.CeilToInt(newItem.AGILITY * .5f));
            MINDMG.AddModifier(newItem.MINDMG);
            MAXDMG.AddModifier(newItem.MAXDMG);
            HITPOINTS.AddModifier(newItem.STRENGHT * 2);
            MANAPOINTS.AddModifier(newItem.INTELIGENCE * 2);
            ARMOR.AddModifier(Mathf.CeilToInt(newItem.STRENGHT * .5f));
        }
        if (oldItem != null)
        {
            STRENGHT.RemoveModifier(oldItem.STRENGHT);
            INTELIGENCE.RemoveModifier(oldItem.INTELIGENCE);
            AGILITY.RemoveModifier(oldItem.AGILITY);
            MOVESPEED.RemoveModifier(Mathf.CeilToInt(oldItem.AGILITY * .5f));
            MINDMG.RemoveModifier(oldItem.MINDMG);
            MAXDMG.RemoveModifier(oldItem.MAXDMG);
            HITPOINTS.RemoveModifier(oldItem.STRENGHT * 2);
            MANAPOINTS.RemoveModifier(oldItem.INTELIGENCE * 2);
            ARMOR.RemoveModifier(Mathf.CeilToInt(oldItem.STRENGHT * .5f));
        }
    }
}
