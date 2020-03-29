using UnityEngine;

public abstract class Equipment_Visual : MonoBehaviour
{
    private GameObject headPoint;
    private GameObject chestPoint;
    private GameObject rightHandPoint;
    private GameObject leftHandPoint;

    [HideInInspector] public GameObject deathBody;
    [HideInInspector] public GameObject aliveBody;

    [Header("Hit Marker")]
    public GameObject hitMarker;

    [Header("Gain Health / Mana etc.")]
    public GameObject healingEffect;
    public GameObject gainManaEffect;

    private GameObject helmet;
    private GameObject chest;
    private GameObject weapon;
    private GameObject offhand;

    public virtual void Start() { }

    public void SetValues()
    {
        headPoint = GetComponentInChildren<Controller_Character_Body>().headPoint;
        chestPoint = GetComponentInChildren<Controller_Character_Body>().chestPoint;
        rightHandPoint = GetComponentInChildren<Controller_Character_Body>().rightHandPoint;
        leftHandPoint = GetComponentInChildren<Controller_Character_Body>().leftHandPoint;

        deathBody = GetComponentInChildren<Controller_Character_Body>().deathBody;
        aliveBody = GetComponentInChildren<Controller_Character_Body>().gameObject;
    }

    public virtual void UpdateVisuals(Item newItem, Item oldItem)
    {
        if (newItem == null)
        {
            if (oldItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                Destroy(weapon);
            }
            else if (oldItem.ITEM_TYPE == ITEMTYPE.OFFHAND || oldItem.ITEM_TYPE == ITEMTYPE.SPELL || oldItem.ITEM_TYPE == ITEMTYPE.ARROW)
            {
                Destroy(offhand);
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
                weapon = Instantiate(newItem.VISUAL_MODEL, rightHandPoint.transform.position, rightHandPoint.transform.rotation);
                weapon.transform.SetParent(rightHandPoint.transform);
            }
            else if (newItem.ITEM_TYPE == ITEMTYPE.OFFHAND || newItem.ITEM_TYPE == ITEMTYPE.SPELL || newItem.ITEM_TYPE == ITEMTYPE.ARROW)
            {
                // here add what to do when you add in the offhand slot a different Arrow type
                offhand = Instantiate(newItem.VISUAL_MODEL, leftHandPoint.transform.position, rightHandPoint.transform.rotation);
                offhand.transform.SetParent(leftHandPoint.transform);
            }
        }
    }

    public virtual void HitMarker(Vector3 where)
    {
        Instantiate(hitMarker, where, transform.rotation); // dont parent the cubes to this gameobject
    }
}