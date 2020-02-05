using UnityEngine;

public class Controller_Equipment : MonoBehaviour
{
    /// <summary>
    /// the array holding the 4 slots for weapon and armor
    /// </summary>    
    public Item[] currentEquipment = new Item[4];

    public virtual void Equip(Item newItem, int index)
    {

        Item oldItem = null;

        if (currentEquipment[index] != null)
        {
            oldItem = currentEquipment[index];
            if (newItem != oldItem)
                Unequip(index);
        }
        else
        {
            oldItem = null;
        }
        currentEquipment[index] = newItem;
    }

    /// <summary>
    /// called if an item needs to be unequiped form a certain index position. 
    /// </summary>
    /// <param name="index">index of the currentEquipment array from where to unequip the item</param>
    public virtual void Unequip(int index)
    {
        if (currentEquipment[index] != null)
        {
            Item oldItem = currentEquipment[index];

            currentEquipment[index] = null;
        }
    }
}
