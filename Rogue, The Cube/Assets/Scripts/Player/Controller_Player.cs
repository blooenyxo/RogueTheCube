using UnityEngine;
using UnityEngine.EventSystems;

public class Controller_Player : MonoBehaviour
{
    private Stats_Player statsPlayer;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 direction;
    private LayerMask floorMask;

    void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        statsPlayer = Stats_Player.instance;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //float angle = Vector3.Angle(movement, direction);
        //Debug.Log(angle);

        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject() && !Input.GetButton("Fire2"))
        {
            if (GetComponentInChildren<Controller_Weapon>())
                GetComponentInChildren<Controller_Weapon>().BaseAttack();
        }

        if (Input.GetButton("Fire2") && !EventSystem.current.IsPointerOverGameObject())
        {
            if (GetComponentInChildren<Controller_Offhand>())
                GetComponentInChildren<Controller_Offhand>().UseOffhand();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            if (GetComponentInChildren<Controller_Offhand>())
                GetComponentInChildren<Controller_Offhand>().ReleaseOffhand();
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        MouseLook();
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * statsPlayer.MOVESPEED.GetValue() * Time.deltaTime;
        //movement = transform.worldToLocalMatrix.inverse * movement;
        rb.MovePosition(transform.position + movement);
    }

    void MouseLook()
    {
        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, floorMask))
        {
            direction = (hit.point - transform.position);
            direction.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (statsPlayer.MOVESPEED.GetValue()
            /* add a slowing modifyer here if needed */ )));
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000f, Color.white);
    }
}