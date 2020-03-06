using UnityEngine;

public class UI_Input : MonoBehaviour
{
    public GameObject CharacterPanel;
    public GameObject NPCPanel;
    public GameObject PickUpPanel;
    public GameObject RemovePanel;
    public GameObject PauseMenu;

    [HideInInspector] public CanvasGroup cp_cg;
    [HideInInspector] public CanvasGroup pp_cg;
    [HideInInspector] public CanvasGroup npc_cg;
    [HideInInspector] public CanvasGroup pm_cg;


    public Camera_Follow cam;

    //private Animator animator_playerstatspanel;
    //private Animator animator_pickuppanel;

    private void Start()
    {
        cp_cg = CharacterPanel.GetComponent<CanvasGroup>();
        pp_cg = PickUpPanel.GetComponent<CanvasGroup>();
        npc_cg = NPCPanel.GetComponent<CanvasGroup>();
        pm_cg = PauseMenu.GetComponent<CanvasGroup>();

        //animator_playerstatspanel = cp_cg.gameObject.GetComponent<Animator>();
        //animator_pickuppanel = pp_cg.gameObject.GetComponent<Animator>();
    }

    public void OpenPlayerPanel()
    {
        cp_cg.alpha = 1;
        cp_cg.blocksRaycasts = true;
        cp_cg.interactable = true;
        //animator_playerstatspanel.SetTrigger("panelopen");
    }
    public void ClosePlayerPanel()
    {
        cp_cg.alpha = 0;
        cp_cg.blocksRaycasts = false;
        cp_cg.interactable = false;
        //animator_playerstatspanel.SetTrigger("panelclose");
    }

    public void OpenPickupPanel()
    {
        pp_cg.alpha = 1;
        pp_cg.blocksRaycasts = true;
        pp_cg.interactable = true;
        //animator_pickuppanel.SetTrigger("ppopen");
    }

    public void ClosePickupPanel()
    {
        pp_cg.alpha = 0;
        pp_cg.blocksRaycasts = false;
        pp_cg.interactable = false;
        //animator_pickuppanel.SetTrigger("ppclose");
    }

    public void OpenNpcPanel(TypeOfNPC _typeOfNPC)
    {
        npc_cg.alpha = 1;
        NPCPanel.GetComponent<NPCPanel_Controller>().Setup(_typeOfNPC);
        npc_cg.blocksRaycasts = true;
        npc_cg.interactable = true;
    }

    public void CloseNpcPanel()
    {
        npc_cg.alpha = 0;
        NPCPanel.GetComponent<NPCPanel_Controller>().ClearNPCPanel();
        npc_cg.blocksRaycasts = false;
        npc_cg.interactable = false;
    }

    public void OpenPauseMenu()
    {
        pm_cg.alpha = 1;
        pm_cg.blocksRaycasts = true;
        pm_cg.interactable = true;
    }

    public void ClosePauseMenu()
    {
        pm_cg.alpha = 0;
        pm_cg.blocksRaycasts = false;
        pm_cg.interactable = false;
    }
}