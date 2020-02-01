using UnityEngine;

public class UI_Input : MonoBehaviour
{
    public GameObject CharacterPanel;
    public GameObject PickUpPanel;
    public GameObject RemovePanel;

    [HideInInspector] public CanvasGroup cp_cg;
    [HideInInspector] public CanvasGroup pp_cg;

    public Camera_Follow cam;

    private void Start()
    {
        cp_cg = CharacterPanel.GetComponent<CanvasGroup>();
        pp_cg = PickUpPanel.GetComponent<CanvasGroup>();

    }

    public void ClosePlayerPanel()
    {
        cp_cg.alpha = 0;
        cp_cg.blocksRaycasts = false;
        cp_cg.interactable = false;
    }

    public void ClosePickupPanel()
    {
        pp_cg.alpha = 0;
        pp_cg.blocksRaycasts = false;
        pp_cg.interactable = false;
    }

    public void OpenPlayerPanel()
    {
        cp_cg.alpha = 1;
        cp_cg.blocksRaycasts = true;
        cp_cg.interactable = true;
    }

    public void OpenPickupPanel()
    {
        pp_cg.alpha = 1;
        pp_cg.blocksRaycasts = true;
        pp_cg.interactable = true;
    }

    public void SwitchPanelState(CanvasGroup cg)
    {
        if (cg.alpha == 0)
        {
            cg.alpha = 1;
        }
        else if (cg.alpha == 1)
        {
            cg.alpha = 0;
        }

        cg.blocksRaycasts = !cg.blocksRaycasts;
        cg.interactable = !cg.interactable;
    }
}
