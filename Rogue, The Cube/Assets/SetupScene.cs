using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupScene : MonoBehaviour
{

    public Dropdown classSelection;
    public Dropdown weaponSelection;

    public void StartGame()
    {
        PlayerPrefs.SetInt("playerClass", classSelection.value);
        PlayerPrefs.SetInt("playerStartingWeapon", weaponSelection.value);

        SceneManager.LoadScene("MainScene");
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
