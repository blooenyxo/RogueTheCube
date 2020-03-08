using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupScene : MonoBehaviour
{
    public InputField nameText;
    public Dropdown classSelection;
    public Dropdown weaponSelection;
    public Button continueButton;

    public static int nrPlayerPrefsSlots = 31;

    private void Start()
    {
        SetupContinueState();
    }

    public void SetupContinueState()
    {
        if (PlayerPrefs.GetInt("continue", 0) == 1)
        {
            continueButton.interactable = true;
            //continueButton.colors = Color.black;
            nameText.text = PlayerPrefs.GetString("PlayerName");
            classSelection.value = PlayerPrefs.GetInt("PlayerClass");
            weaponSelection.value = PlayerPrefs.GetInt("PlayerWeapons");
        }
        else
        {
            continueButton.interactable = false;
            nameText.text = null;
            classSelection.value = 0;
            weaponSelection.value = 0;
        }
    }

    public void StartGame()
    {
        ResetGameProgress();
        PlayerPrefs.SetInt("PlayerClass", classSelection.value);
        PlayerPrefs.SetInt("PlayerWeapons", weaponSelection.value);
        PlayerPrefs.SetString("PlayerName", nameText.text);

        if (weaponSelection.value == 0)
        {
            SetPlayerPrefsItems(30, 1, "Wooden Sword", 0);
            SetPlayerPrefsItems(31, 1, "Wooden Shield", 0);
        }
        else if (weaponSelection.value == 1)
        {
            SetPlayerPrefsItems(30, 1, "Short Bow", 0);
            SetPlayerPrefsItems(31, 1, "Fire Arrow", 10);
        }
        else if (weaponSelection.value == 2)
        {
            SetPlayerPrefsItems(30, 1, "Base Staff", 0);
            SetPlayerPrefsItems(31, 1, "Deathball", 0);
        }

        SetPlayerPrefsItems(0, 1, "Small Potion of Healing", 5);
        SetPlayerPrefsItems(1, 1, "Small Potion of Mana", 5);
        SetPlayerPrefsItems(27, 1, "Small Rejuvenation Potion", 10);

        PlayerPrefs.SetInt("continue", 1);
        SceneManager.LoadScene("MainScene");
    }

    public void SetPlayerPrefsItems(int position, int activeState, string itemName, int stacks)
    {
        // creates a new player prefs string to be used with startgame script
        // map: 0 - 23 - inventory / 24 - 27 - hotbar / 28 - 31 - equipment
        // map: 28 - Head / 29 - Chest / 30 - Weapon / 31 - Offhand
        // itemname: this should be set here only on game start. the initial equipment.
        // activeState: 0 - false / 1 - true
        PlayerPrefs.SetInt("playerInvSlot" + position + "state", activeState);
        PlayerPrefs.SetString("playerInvSlot" + position + "item", itemName);
        PlayerPrefs.SetInt("playerInvSlot" + position + "stacks", stacks);
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.GetInt("continue", 0) == 1)
            SceneManager.LoadScene("MainScene");
    }

    public void ResetGameProgress()
    {
        continueButton.interactable = false;

        for (int i = 0; i <= nrPlayerPrefsSlots; i++)
        {
            PlayerPrefs.SetInt("playerInvSlot" + i + "state", 0);
        }
        PlayerPrefs.SetInt("PlayerClass", 0);
        PlayerPrefs.SetInt("CurrentGold", 0);
        PlayerPrefs.SetInt("continue", 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}