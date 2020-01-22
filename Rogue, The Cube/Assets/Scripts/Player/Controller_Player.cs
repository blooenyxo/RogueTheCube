using UnityEngine;
using UnityEngine.EventSystems;

public class Controller_Player : MonoBehaviour
{
    private Stats_Player statsPlayer;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 direction;
    private LayerMask floorMask;
    private UI_Input ui_input;

    public LayerMask interactionLayer;

    void Start()
    {
        ui_input = GameObject.Find("UI_Canvas").GetComponent<UI_Input>();
        floorMask = LayerMask.GetMask("Floor");
        statsPlayer = Stats_Player.instance;
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// all the player inputs should be controlled from here. movement, interaction, ui, combat
    /// </summary>
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

        if (Input.GetKeyDown(KeyCode.E) && NearbyInteraction() != null)
        {
            if (NearbyInteraction().CompareTag("NPC"))
            {
                // npc interaction starts here
                Debug.Log(NearbyInteraction());
            }
            else if (NearbyInteraction().CompareTag("LootBox"))
            {
                ui_input.SwitchPanelState(ui_input.pp_cg);
                if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == false)
                    ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().SetupPanel(NearbyInteraction());
                else if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == true)
                    ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
            }
        }

        if (NearbyInteraction() == null)
        {
            ui_input.ClosePickupPanel();
            ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
        }




        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    ui_input.SwitchPanelState(ui_input.pp_cg);
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            ui_input.SwitchPanelState(ui_input.cp_cg);
        }

        if (Input.GetButtonDown("Jump"))
        {
            ui_input.ClosePickupPanel();
            ui_input.ClosePlayerPanel();
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

    public GameObject NearbyInteraction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 3f, interactionLayer);

        if (hitColliders.Length > 0)
        {
            foreach (Collider col in hitColliders)
            {
                return col.gameObject;
            }
        }
        else
        {
            return null;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000f, Color.white);
    }
}