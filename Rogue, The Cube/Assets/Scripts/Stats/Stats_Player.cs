using UnityEngine;

public class Stats_Player : Stats
{
    #region Singelton
    public static Stats_Player instance;
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

    public delegate void OnStatsChanged();
    public OnStatsChanged onStatsChanged;

    public delegate void OnResourcesChanged();
    public OnResourcesChanged onResourcesChanged;

    Equipment equipment;
    /// <summary>
    /// holds the method to adjust stats based on currently equiped items. delegate triggers on item add or 
    /// item removed with the respective parameters.
    /// </summary>
    public override void Start()
    {
        equipment = Equipment.instance;
        equipment.onEquipmentChange += OnEquipmentChange;
        base.Start();
    }

    /// <summary>
    /// every new Stat added to the game needs a entry added here. "stat.addModifier(newItem.stat);" and "stat.removeModifier(oldItem.stat);"
    /// </summary>
    /// <param name="newItem">the the item which stats are about to be added</param>
    /// <param name="oldItem">the the item currently in slot which stats are about to be removed</param>
    public override void OnEquipmentChange(Item newItem, Item oldItem)
    {
        base.OnEquipmentChange(newItem, oldItem);
        onStatsChanged.Invoke();
        onResourcesChanged.Invoke();
    }

    public override void TakeDamage(int value)
    {
        base.TakeDamage(value);
        onResourcesChanged.Invoke();
    }

    public override void Heal(int value)
    {
        base.Heal(value);
        onResourcesChanged.Invoke();
    }

    public override void GainMana(int value)
    {
        base.GainMana(value);
        onResourcesChanged.Invoke();
    }

    public override bool UseMana(int value)
    {
        onResourcesChanged.Invoke();
        return base.UseMana(value);
    }

    public override bool UseStamina(int value)
    {
        return base.UseStamina(value);
    }

    public override void GainStamina(int value)
    {
        base.GainStamina(value);
        onResourcesChanged.Invoke();
    }
}