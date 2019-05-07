using UnityEngine;

/// <summary>
/// contains two methods for equiping and unequiping items into the equipment array. 
/// it contains a delegate to invoke when an item was added to the equipment array.
/// </summary>
public class Equipment : MonoBehaviour {
    #region Singelton
    public static Equipment instance;
    void Awake () {
        if (instance != null) {
            Debug.LogWarning ("More then one Inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    /// <summary>
    /// the array holding the 4 slots for weapon and armor
    /// </summary>    
    public Item[] currentEquipment = new Item[4];

    public delegate void OnEquipmentChange (Item newItem, Item oldItem);
    /// <summary>
    /// triggered when an item was added or remoed from the equipment array.
    /// currently only useed in the Stats_Player Class.
    /// </summary>
    public OnEquipmentChange onEquipmentChange;

    /// <summary>
    /// called when an item is equiped. checks if there is a item in the same equipment slot. if so, it sends it to be 
    /// unequiped. otherwise, equipes the first item into the indexed slot.
    /// </summary>
    /// <param name="newItem">item sent to the equipment array</param>
    /// <param name="index">index of the item. used for choosing in which slot to equip the item</param>
    public void Equip (Item newItem, int index) {

        Item oldItem = null;

        if (currentEquipment[index] != null) {
            oldItem = currentEquipment[index];
            if (newItem != oldItem)
                Unequip (index);
        } else {
            oldItem = null;
        }
        currentEquipment[index] = newItem;

        if (onEquipmentChange != null)
            onEquipmentChange.Invoke (newItem, oldItem);
    }

    /// <summary>
    /// called if an item needs to be unequiped form a certain index position. 
    /// </summary>
    /// <param name="index">index of the currentEquipment array from where to unequip the item</param>
    public void Unequip (int index) {
        if (currentEquipment[index] != null) {
            Item oldItem = currentEquipment[index];

            currentEquipment[index] = null;

            if (onEquipmentChange != null)
                onEquipmentChange.Invoke (null, oldItem);
        }
    }
}