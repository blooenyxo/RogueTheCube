using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Input_Attacking : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject() && !Input.GetButton("Fire2"))
        {
            if (GetComponentInChildren<Controller_Weapon>())
                GetComponentInChildren<Controller_Weapon>().BaseAttack();
        }

        if (Input.GetButton("Fire2") && !EventSystem.current.IsPointerOverGameObject() && !Input.GetButton("Fire1"))
        {
            if (GetComponentInChildren<Controller_Offhand>())
                GetComponentInChildren<Controller_Offhand>().UseOffhand();
        }

        if (Input.GetButtonUp("Fire2") && !EventSystem.current.IsPointerOverGameObject())
        {
            if (GetComponentInChildren<Controller_Offhand>())
                GetComponentInChildren<Controller_Offhand>().ReleaseOffhand();
        }
    }
}