using UnityEngine;

public class Equipment_Visual_Enemy : Equipment_Visual
{
    [Header("Enemy Death")]
    public GameObject lootBox;

    [HideInInspector] public Controller_Equipment equipment;
    //[HideInInspector] public Enemy enemy;

    public override void Start()
    {
        base.Start();
    }

    public void EnemyUpdateVisuals()
    {
        equipment = GetComponent<Controller_Equipment>();

        if (equipment != null)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (equipment.currentEquipment[i] != null)
                    UpdateVisuals(equipment.currentEquipment[i], null);
            }
        }
    }

    public override void UpdateVisuals(Item newItem, Item oldItem)
    {
        base.UpdateVisuals(newItem, oldItem);
    }
}