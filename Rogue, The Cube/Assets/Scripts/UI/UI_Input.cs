using UnityEngine;

public class UI_Input : MonoBehaviour
{
    public GameObject CharacterPanel;
    public GameObject PickUpPanel;
    public GameObject RemovePanel;

    [HideInInspector] public CanvasGroup cp_cg;
    [HideInInspector] public CanvasGroup pp_cg;

    public Camera_Follow cam;

    private Animator animator_playerstatspanel;
    private Animator animator_pickuppanel;

    private bool pickuppanelopen;

    private void Start()
    {
        cp_cg = CharacterPanel.GetComponent<CanvasGroup>();
        pp_cg = PickUpPanel.GetComponent<CanvasGroup>();

        animator_playerstatspanel = cp_cg.gameObject.GetComponent<Animator>();
        animator_pickuppanel = pp_cg.gameObject.GetComponent<Animator>();
    }

    public void OpenPlayerPanel()
    {
        cp_cg.blocksRaycasts = true;
        cp_cg.interactable = true;

        animator_playerstatspanel.SetTrigger("panelopen");
    }
    public void ClosePlayerPanel()
    {
        cp_cg.blocksRaycasts = false;
        cp_cg.interactable = false;

        animator_playerstatspanel.SetTrigger("panelclose");
    }

    public void OpenPickupPanel()
    {
        pp_cg.blocksRaycasts = true;
        pp_cg.interactable = true;
        animator_pickuppanel.SetTrigger("ppopen");
        pickuppanelopen = true;
    }

    public void ClosePickupPanel()
    {
        pp_cg.blocksRaycasts = false;
        pp_cg.interactable = false;
        animator_pickuppanel.SetTrigger("ppclose");
        pickuppanelopen = false;
    }

    public void SwitchPanelState(CanvasGroup cg)
    {
        if (pickuppanelopen)
        {
            ClosePickupPanel();
        }
        else if (!pickuppanelopen)
        {
            OpenPickupPanel();
        }
    }
}
