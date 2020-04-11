using UnityEngine;
using UnityEngine.EventSystems;

public class Controller_Input : MonoBehaviour
{
    private Stats_Player statsPlayer;
    private Controller_Equipment equipment;
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 direction;
    private LayerMask floorMask;
    private UI_Input ui_input;

    public bool uiIsOpen = false;
    [HideInInspector] public bool pickupPanelOpen = false;
    [HideInInspector] public bool inventoryPanelOpen = false;
    [HideInInspector] public bool npcPanelOpen = false;
    [HideInInspector] public bool pauseMenuOpen = false;
    public LayerMask interactionLayer;
    public float speedModifyer = 0f;
    private float _speedModifyer = 1f;



    void Start()
    {
        ui_input = GameObject.Find("UI_Canvas").GetComponent<UI_Input>();
        floorMask = LayerMask.GetMask("CameraLook");
        statsPlayer = Stats_Player.instance;
        equipment = GetComponent<Controller_Equipment>();
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

        if (Input.GetButtonUp("Fire2") && !EventSystem.current.IsPointerOverGameObject())
        {
            if (GetComponentInChildren<Controller_Offhand>())
                GetComponentInChildren<Controller_Offhand>().ReleaseOffhand();
        }

        #region Special Input
        /*

        ?&

        if (Input.GetButton("Fire2") && !EventSystem.current.IsPointerOverGameObject())
        {

            // i dont think this one will work. need a rework on this system, always use the offhand controller. should be easyer

            //if (equipment.currentEquipment[2] != null && equipment.currentEquipment[2].TwoHandetWeapon == true)
            //{
            //    if (GetComponentInChildren<Controller_Weapon>())
            //        GetComponentInChildren<Controller_Weapon>().SpecialAttack();
            //}
            //else if (equipment.currentEquipment[2] == null || equipment.currentEquipment[2].TwoHandetWeapon == false)
            //{
            //    if (GetComponentInChildren<Controller_Offhand>())
            //        GetComponentInChildren<Controller_Offhand>().UseOffhand();
            //}

            if (GetComponentInChildren<Controller_Offhand>())
                GetComponentInChildren<Controller_Offhand>().UseOffhand();
        }
        else if (Input.GetButtonUp("Fire2") && !EventSystem.current.IsPointerOverGameObject())
        {
            if (equipment.currentEquipment[2] != null && equipment.currentEquipment[2].TwoHandetWeapon == true)
            {
                return;
            }
            else if (equipment.currentEquipment[2] == null || equipment.currentEquipment[2].TwoHandetWeapon == false)
            {
                if (GetComponentInChildren<Controller_Offhand>())
                    GetComponentInChildren<Controller_Offhand>().ReleaseOffhand();
            }
        }

        */
        #endregion

        if (Input.GetButtonDown("Interact") && NearbyInteraction() != null)
        {
            if (NearbyInteraction().CompareTag("NPC"))
            {
                SwitchNPCPanelState();
                ui_input.OpenPlayerPanel();
                inventoryPanelOpen = true;
            }
            else if (NearbyInteraction().CompareTag("LootBox"))
            {
                SwitchLootBoxState();
            }
        }

        if (Input.GetButtonDown("Inventory"))
        {
            SwitchInventoryState();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (uiIsOpen || inventoryPanelOpen || pickupPanelOpen || npcPanelOpen || pauseMenuOpen)
            {
                uiIsOpen = false;
                inventoryPanelOpen = false;
                pickupPanelOpen = false;
                npcPanelOpen = false;
                pauseMenuOpen = false;

                ui_input.ClosePickupPanel();
                ui_input.ClosePlayerPanel();
                ui_input.CloseNpcPanel();
                ui_input.ClosePauseMenu();
            }
            else
            {
                // open quit menu
                pauseMenuOpen = true;
                uiIsOpen = true;
                ui_input.OpenPauseMenu();
            }
        }

        if (NearbyInteraction() == null && npcPanelOpen)
        {
            ui_input.CloseNpcPanel();
            ui_input.ClosePlayerPanel();
            npcPanelOpen = false;
            inventoryPanelOpen = false;
        }

        if (NearbyInteraction() == null && pickupPanelOpen)
        {
            ui_input.ClosePickupPanel();
            ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
            pickupPanelOpen = false;
        }

        // uiisopen bool turns true every frame when one of the panels is open (inv., pickup, and all the future ones (dialog, shop etc..))
        if (inventoryPanelOpen || pickupPanelOpen || npcPanelOpen || pauseMenuOpen)
            uiIsOpen = true;
        else
            uiIsOpen = false;
    }

    public void SwitchInventoryState()
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

    public void SwitchLootBoxState()
    {
        if (!pickupPanelOpen)
        {
            ui_input.OpenPickupPanel();
            pickupPanelOpen = true;

            if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == false)
            {
                ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().SetupPanel(NearbyInteraction());
                NearbyInteraction().GetComponent<LootBox_Controller>().LootboxOpen();
            }
        }
        else if (pickupPanelOpen)
        {
            ui_input.ClosePickupPanel();
            pickupPanelOpen = false;

            if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == true)
            {
                ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
            }
        }
    }

    public void SwitchNPCPanelState()
    {
        if (npcPanelOpen)
        {
            ui_input.CloseNpcPanel();
            //ui_input.ClosePlayerPanel();
            npcPanelOpen = false;
        }
        else if (!npcPanelOpen)
        {
            // npc interaction starts here
            ui_input.OpenNpcPanel(NearbyInteraction().GetComponent<NPC_Controller>().typeOfNPC); // switch this possibly to enums (done that)
            npcPanelOpen = true;
        }
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
                if (statsPlayer.UseStamina(1))
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

    public GameObject NearbyInteraction() // return the closest collider to the sphere center
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f, interactionLayer);

        if (hitColliders.Length > 0)
        {
            return hitColliders[0].gameObject;
        }
        else
        {
            return null;
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000f, Color.white);
    }
}