using UnityEngine;

public class Equipment_Visual_Enemy : Equipment_Visual
{
    [Header("Enemy Death")]
    public GameObject lootBox;

    [HideInInspector] public Controller_Equipment equipment;

    public override void Start()
    {
        equipment = GetComponent<Controller_Equipment>();

        if (equipment != null)
        {
            if (equipment.currentEquipment[2] != null)
                UpdateVisuals(equipment.currentEquipment[2], null);
            if (equipment.currentEquipment[1] != null)
                UpdateVisuals(equipment.currentEquipment[1], null);
        }
    }

    public override void UpdateVisuals(Item newItem, Item oldItem)
    {
        base.UpdateVisuals(newItem, oldItem);
    }
}