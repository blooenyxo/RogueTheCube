using UnityEngine;

public abstract class Equipment_Visual : MonoBehaviour
{
    [Header("Equipment Slots")]
    public GameObject rightHandPoint;
    public GameObject leftHandPoint;
    public GameObject headPoint;

    [Header("Dead")]
    public GameObject deathBody;
    public GameObject aliveBody;

    [Header("HitMarker")]
    public GameObject hitMarker;

    GameObject weapon;
    GameObject offhand;

    public virtual void Start() { }

    /// <summary>
    /// for testing only
    /// </summary>
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        HitMarker(transform);
    //    }
    //}

    public virtual void UpdateVisuals(Item newItem, Item oldItem)
    {
        if (newItem == null)
        {
            if (oldItem.ITEM_TYPE == ITEMTYPE.WEAPON)
            {
                Destroy(weapon);
            }
            else if (oldItem.ITEM_TYPE == ITEMTYPE.OFFHAND)
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
            else if (newItem.ITEM_TYPE == ITEMTYPE.OFFHAND)
            {
                offhand = Instantiate(newItem.VISUAL_MODEL, leftHandPoint.transform.position, rightHandPoint.transform.rotation);
                offhand.transform.SetParent(leftHandPoint.transform);
            }
        }
    }

    public virtual void HitMarker(Vector3 where)
    {
        GameObject hm = Instantiate(hitMarker, where, Quaternion.identity);
        //hm.transform.SetParent(this.transform); // this made the cubes behave odd. create a cube container and spanw the blood cubes in it
    }
}