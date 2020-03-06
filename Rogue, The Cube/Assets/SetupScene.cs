using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupScene : MonoBehaviour
{

    public Dropdown classSelection;
    public Dropdown weaponSelection;
    public Button continueButton;

    private void Start()
    {
        if (PlayerPrefs.GetInt("continue", 0) == 1)
        {
            continueButton.interactable = true;
            //continueButton.colors = Color.black;
        }
        else
            continueButton.interactable = false;
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("playerClass", classSelection.value);
        PlayerPrefs.SetInt("playerStartingWeapon", weaponSelection.value);
        PlayerPrefs.SetInt("continue", 1);

        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("continue", 0) == 1)
            SceneManager.LoadScene("MainScene");
    }

    public void ResetGameProgress()
    {
        continueButton.interactable = false;
        PlayerPrefs.SetInt("continue", 0);
    }
}