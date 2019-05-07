using UnityEngine;

public class Stats_Player : Stats {
    #region Singelton
    public static Stats_Player instance;
    void Awake () {
        if (instance != null) {
            Debug.LogWarning ("More then one Inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnStatsChanged ();
    public OnStatsChanged onStatsChanged;

    Equipment equipment;
    /// <summary>
    /// holds the methode to adjust stats based on currently equiped items. delegate triggers on item add or 
    /// item removed with the respective parameters.
    /// </summary>
    private void Start () {
        equipment = Equipment.instance;
        equipment.onEquipmentChange += OnEquipmentChange;
    }

    /// <summary>
    /// every new Stat added to the game needs a entry added here. "stat.addModifier(newItem.stat);" and "stat.removeModifier(oldItem.stat);"
    /// </summary>
    /// <param name="newItem">the the item which stats are about to be added</param>
    /// <param name="oldItem">the the item currently in slot which stats are about to be removed</param>
    public override void OnEquipmentChange (Item newItem, Item oldItem) {
        base.OnEquipmentChange (newItem, oldItem);
        onStatsChanged.Invoke ();
    }
}