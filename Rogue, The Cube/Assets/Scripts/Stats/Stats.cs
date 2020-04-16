using System.Collections;
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
    public Stat MINMAGIC;
    public Stat MAXMAGIC;
    public Stat STAMINA;
    public Stat GOLD;

    public int CurrentHealth { get; private set; }
    public int CurrentMana { get; private set; }
    public int CurrentStamina { get; private set; }
    public int CurrentGold { get; private set; }

    public virtual void Start()
    {
        SetAllToMax();
    }

    public virtual void SetAllToMax()
    {
        CurrentHealth = HITPOINTS.GetValue();
        CurrentMana = MANAPOINTS.GetValue();
        //CurrentStamina = STAMINA.GetValue();
        CurrentGold = GOLD.GetValue();
    }

    /// <summary>
    /// Health Stuff
    /// </summary>
    //TODO adjust the importance of armor here
    public virtual void TakeDamage(int value)
    {
        value -= ARMOR.GetValue();
        value = Mathf.Clamp(value, 1, int.MaxValue);
        CurrentHealth -= value;

        //Debug.Log(this.gameObject.name + " Took " + value + " damage");

        if (CurrentHealth <= 0)
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
        int i = Random.Range(1, 101);
        if (i > CritChance())
        {
            return BaseDamage();
        }
        else
        {
            return Mathf.RoundToInt(BaseDamage() + CritAmount());
        }
    }

    private int BaseDamage()
    {
        return Mathf.CeilToInt(Random.Range(MINDMG.GetValue(), MAXDMG.GetValue()) + (STRENGHT.GetValue() * .1f));
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

    public virtual int DealMagicDamage()
    {
        return Mathf.CeilToInt(Random.Range(MINMAGIC.GetValue(), MAXMAGIC.GetValue()) + (INTELIGENCE.GetValue() * .1f));
    }

    public virtual void Heal(int value)
    {
        CurrentHealth += value;
        if (CurrentHealth > HITPOINTS.GetValue())
        {
            CurrentHealth = HITPOINTS.GetValue();
        }
    }
    /// <summary>
    /// Mana Stuff
    /// </summary>
    public virtual void GainMana(int value)
    {
        CurrentMana += value;
        if (CurrentMana > MANAPOINTS.GetValue())
        {
            CurrentMana = MANAPOINTS.GetValue();
        }
    }

    public virtual bool UseMana(int value)
    {
        if (CurrentMana >= value)
        {
            CurrentMana -= value;
            return true;
        }
        return false;
    }
    public virtual void Die()
    {
        /// destroy the graphics of the character, spanw the dead body prefab with another parent, (and then destroy the whole gameObject <- this was moved to stats_enemy, dont fully destroy the player)
        if (GetComponent<Equipment_Visual>())
        {
            Equipment_Visual ev = GetComponent<Equipment_Visual>();
            Destroy(ev.aliveBody);
            Instantiate(ev.deathBody, this.transform.position, this.transform.rotation);
        }
    }

    public virtual bool UseStamina(int value)
    {
        if (CurrentStamina > value)
        {
            CurrentStamina -= value;
            return true;
        }
        return false;
    }

    public virtual void GainStamina(int value)
    {
        CurrentStamina += value;
        if (CurrentStamina >= STAMINA.GetValue())
        {
            CurrentStamina = STAMINA.GetValue();
        }
    }

    public virtual void SetMovespeed(int value)
    {
        MOVESPEED.AddModifier(value);
    }

    public virtual void ResetMovespeed(int value)
    {
        MOVESPEED.RemoveModifier(value);
    }

    public IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(1f);
    }

    public virtual void GainGold(int value)
    {
        CurrentGold += value;
    }

    public virtual bool UseGold(int value)
    {
        if (CurrentGold >= value)
        {
            CurrentGold -= value;
            return true;
        }
        else
            return false;
    }

    #region OnEquipmentChange

    public virtual void OnEquipmentChange(Item newItem, Item oldItem)
    {
        if (newItem != null)
        {
            STRENGHT.AddModifier(newItem.STRENGHT);
            INTELIGENCE.AddModifier(newItem.INTELIGENCE);
            AGILITY.AddModifier(newItem.AGILITY);
            MOVESPEED.AddModifier(Mathf.CeilToInt(newItem.AGILITY * .1f));
            MINDMG.AddModifier(newItem.MINDMG);
            MAXDMG.AddModifier(newItem.MAXDMG);
            MINMAGIC.AddModifier(newItem.MINMAGIC);
            MAXMAGIC.AddModifier(newItem.MAXMAGIC);
            HITPOINTS.AddModifier(newItem.STRENGHT * 2);
            MANAPOINTS.AddModifier(newItem.INTELIGENCE * 2);
            ARMOR.AddModifier(Mathf.CeilToInt(newItem.STRENGHT * .5f));
        }
        if (oldItem != null)
        {
            STRENGHT.RemoveModifier(oldItem.STRENGHT);
            INTELIGENCE.RemoveModifier(oldItem.INTELIGENCE);
            AGILITY.RemoveModifier(oldItem.AGILITY);
            MOVESPEED.RemoveModifier(Mathf.CeilToInt(oldItem.AGILITY * .1f));
            MINDMG.RemoveModifier(oldItem.MINDMG);
            MAXDMG.RemoveModifier(oldItem.MAXDMG);
            MINMAGIC.RemoveModifier(oldItem.MINMAGIC);
            MAXMAGIC.RemoveModifier(oldItem.MAXMAGIC);
            HITPOINTS.RemoveModifier(oldItem.STRENGHT * 2);
            MANAPOINTS.RemoveModifier(oldItem.INTELIGENCE * 2);
            ARMOR.RemoveModifier(Mathf.CeilToInt(oldItem.STRENGHT * .5f));
        }
    }

    #endregion
}
