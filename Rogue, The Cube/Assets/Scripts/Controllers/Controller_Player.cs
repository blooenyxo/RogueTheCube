using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    private Stats_Player statsPlayer;
    private Rigidbody rb;
    private Vector3 movement;

    public KeyCode inspectKey;

    void Start()
    {
        statsPlayer = Stats_Player.instance;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(inspectKey))
        {

        }
        else
        {

        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (GetComponentInChildren<Controller_Weapon>())
                GetComponentInChildren<Controller_Weapon>().BaseAttack();
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
        rb.MovePosition(transform.position + movement);
    }

    void MouseLook()
    {
        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, 9))
        {
            Vector3 direction = (hit.point - transform.position);
            direction.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * (statsPlayer.MOVESPEED.GetValue())));
        }
    }
}