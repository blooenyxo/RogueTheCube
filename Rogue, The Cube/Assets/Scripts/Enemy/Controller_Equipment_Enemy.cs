using UnityEngine;

public class Controller_Equipment_Enemy : Controller_Equipment
{
    [HideInInspector] public Enemy enemy;

    public void SetValues()
    {
        currentEquipment[0] = enemy.headGear;
        currentEquipment[1] = enemy.chestGear;
        currentEquipment[2] = enemy.weapon;
        currentEquipment[3] = enemy.offhand;
    }
}