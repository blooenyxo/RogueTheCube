using UnityEngine;

public class UI_Input : MonoBehaviour
{
    public GameObject CharacterPanel;
    public GameObject PickUpPanel;
    private CanvasGroup cp_cg;

    private void Start()
    {
        cp_cg = CharacterPanel.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpPanel.SetActive(!PickUpPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (cp_cg.alpha == 0)
            {
                OpenPlayerPanel();
            }
            else if (cp_cg.alpha == 1)
            {
                ClosePlayerPanel();
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            PickUpPanel.SetActive(false);
            ClosePlayerPanel();
        }
    }

    void ClosePlayerPanel()
    {
        cp_cg.alpha = 0;
        cp_cg.blocksRaycasts = false;
        cp_cg.interactable = false;
    }

    void OpenPlayerPanel()
    {
        cp_cg.alpha = 1;
        cp_cg.blocksRaycasts = true;
        cp_cg.interactable = true;
    }
}
