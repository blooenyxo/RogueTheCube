using UnityEngine;

public class Controller_Equipment_Enemy : Controller_Equipment
{
    public void SetValues(Enemy enemy)
    {
        currentEquipment[0] = enemy.headGear;
        currentEquipment[1] = enemy.chestGear;
        currentEquipment[2] = enemy.weapon;
        currentEquipment[3] = enemy.offhand;
    }
}