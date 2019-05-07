using UnityEngine;

public class UI_Input : MonoBehaviour
{
    public GameObject CharacterPanel;
    public GameObject PickUpPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PickUpPanel.SetActive(!PickUpPanel.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CharacterPanel.SetActive(!CharacterPanel.activeSelf);
        }

        if (Input.GetButtonDown("Jump"))
        {
            PickUpPanel.SetActive(false);
            CharacterPanel.SetActive(false);
        }
    }
}
