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
    private bool pickupPanelOpen = false;
    private bool inventoryPanelOpen = false;

    [HideInInspector] public bool uiIsOpen = false;
    public LayerMask interactionLayer;
    public float speedModifyer = 0f;
    private float _speedModifyer = 1f;

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
                {
                    ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().SetupPanel(NearbyInteraction());
                    pickupPanelOpen = true;
                }
                else if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == true)
                {
                    ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
                    pickupPanelOpen = false;
                }
            }
        }

        if (NearbyInteraction() == null && pickupPanelOpen)
        {
            ui_input.ClosePickupPanel();
            ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
            pickupPanelOpen = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (inventoryPanelOpen == false)
            {
                ui_input.OpenPlayerPanel();
                inventoryPanelOpen = true;
            }
            else
            {
                ui_input.ClosePlayerPanel();
                inventoryPanelOpen = false;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            //uiIsOpen = false;
            //inventoryPanelOpen = false;
            //pickupPanelOpen = false;
            //ui_input.ClosePickupPanel();
            //ui_input.ClosePlayerPanel();
        }

        // uiisopen bool turns true every frame when one of the panels is open (inv., pickup, and all the future ones (dialog, shop etc..))
        if (inventoryPanelOpen || pickupPanelOpen)
            uiIsOpen = true;
        else
            uiIsOpen = false;


    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");



        if (Input.GetAxisRaw("Vertical") > 0.1f)
        {
            _speedModifyer = 1;
            if (Input.GetButton("Run"))
            {
                //Debug.Log("running");
                if (statsPlayer.UseStamina(Mathf.CeilToInt(1 * Time.deltaTime)))
                {
                    _speedModifyer = speedModifyer;
                    statsPlayer.onResourcesChanged.Invoke();
                }
                else
                {
                    _speedModifyer = 1;
                }
            }
            else if (Input.GetButtonUp("Run"))
            {
                //Debug.Log("not running");
                _speedModifyer = 1;
            }
        }
        else if (Input.GetAxisRaw("Vertical") < 0.1f)
        {
            _speedModifyer = 0.5f;
        }
        else if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            _speedModifyer = 0.5f;
        }

        if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)
        {
            _speedModifyer = 1;
        }

        if (!Input.GetButton("Run"))
        {
            statsPlayer.GainStamina(1);
        }

        Move(h, v);

        // this check is ment to make you not able to move / turn while inv or any other panel is open
        // i dont like how this works, so i keep testing it.
        if (!uiIsOpen)
        {
            MouseLook();
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * (statsPlayer.MOVESPEED.GetValue() * Time.deltaTime * _speedModifyer);
        movement = transform.worldToLocalMatrix.inverse * movement;
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
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * statsPlayer.MOVESPEED.GetValue()
            /* add a turn slowing modifyer here if needed */  ));
        }
    }

    public GameObject NearbyInteraction()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 2f, interactionLayer);

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