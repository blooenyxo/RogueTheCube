using UnityEngine;

public class Equipment_Visual : MonoBehaviour
{
    [Header("Equipment Slots")]
    public GameObject rightHandPoint;
    public GameObject leftHandPoint;
    public GameObject headPoint;

    [Header("Dead")]
    public GameObject deathBody;
    public GameObject aliveBody;

    Equipment equipment;
    GameObject weapon;

    public virtual void Start()
    {
        equipment = Equipment.instance;
        equipment.onEquipmentChange += UpdateVisuals;
    }

    public virtual void UpdateVisuals(Item newItem, Item oldItem)
    {
        if (newItem == null)
        {
            if (oldItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                Destroy(weapon);
            }
        }
        else
        {
            if (newItem == oldItem)
            {
                return;
            }

            if (newItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                weapon = Instantiate(newItem.VISUAL_WEAPON, rightHandPoint.transform.position, rightHandPoint.transform.rotation);
                weapon.transform.SetParent(rightHandPoint.transform);
            }
        }
    }
}