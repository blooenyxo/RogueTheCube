using UnityEngine;

public class Player_Input_UI : MonoBehaviour
{
    public LayerMask interactionLayer;
    public MouseLook mouseLook;
    //public bool ui_panel_state = false;

    private UI_Input ui_input;
    private bool npc_panel_state = false;
    private bool lootbox_panel_state = false;
    private bool invetory_panel_state = false;
    private bool pause_panel_state = false;

    private void Start()
    {
        ui_input = GameObject.Find("UI_Canvas").GetComponent<UI_Input>();
        mouseLook = GetComponentInChildren<MouseLook>();
    }

    // add every panel to the Cancel action + make for every new panel a "...PanelState" methode.
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && NearbyInteraction() != null)
        {
            if (NearbyInteraction().CompareTag("NPC"))
            {
                NPCPanelState();
                if (!invetory_panel_state)
                    InventoryPanelState();
            }
            else if (NearbyInteraction().CompareTag("LootBox"))
            {
                LootboxPanelState();
                if (!invetory_panel_state)
                    InventoryPanelState();
            }
        }

        if (Input.GetButtonDown("Inventory"))
        {
            InventoryPanelState();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (!npc_panel_state && !lootbox_panel_state && !invetory_panel_state)
                PauseMenuState();

            if (npc_panel_state)
                NPCPanelState();
            if (lootbox_panel_state)
                LootboxPanelState();
            if (invetory_panel_state)
                InventoryPanelState();
        }

        if (NearbyInteraction() == null)
        {
            npc_panel_state = false;
            lootbox_panel_state = false;
            ui_input.CloseNpcPanel();
            ui_input.ClosePickupPanel();
        }

        if (npc_panel_state || lootbox_panel_state || invetory_panel_state || pause_panel_state)
        {
            mouseLook.uiOpen = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            mouseLook.uiOpen = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void PauseMenuState()
    {
        pause_panel_state = !pause_panel_state;
        if (pause_panel_state)
        {
            ui_input.OpenPauseMenu();
        }
        else if (!pause_panel_state)
        {
            ui_input.ClosePauseMenu();
        }
    }

    private void InventoryPanelState()
    {
        invetory_panel_state = !invetory_panel_state;
        if (invetory_panel_state)
        {
            ui_input.OpenPlayerPanel();
        }
        else if (!invetory_panel_state)
        {
            ui_input.ClosePlayerPanel();
        }
    }

    private void NPCPanelState()
    {
        npc_panel_state = !npc_panel_state;
        if (npc_panel_state)
        {
            ui_input.OpenNpcPanel(NearbyInteraction().GetComponent<NPC_Controller>().typeOfNPC);
        }
        else if (!npc_panel_state)
        {
            ui_input.CloseNpcPanel();
        }
    }

    private void LootboxPanelState()
    {
        lootbox_panel_state = !lootbox_panel_state;
        if (lootbox_panel_state)
        {
            ui_input.OpenPickupPanel();

            if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == false)
            {
                ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().SetupPanel(NearbyInteraction());
                NearbyInteraction().GetComponent<LootBox_Controller>().LootboxOpen();
            }
        }
        else if (!lootbox_panel_state)
        {
            ui_input.ClosePickupPanel();

            if (ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().loadedInPanel == true)
            {
                ui_input.gameObject.GetComponentInChildren<PickupPanel_Controller>().RemoveItemFromPanel();
            }
        }
    }

    public GameObject NearbyInteraction() // return the closest collider to the sphere center, where the spehere is centered on the player
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
}