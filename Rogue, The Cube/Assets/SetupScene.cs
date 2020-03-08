using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupScene : MonoBehaviour
{

    public Dropdown classSelection;
    public Dropdown weaponSelection;
    public Button continueButton;

    public static int nrPlayerPrefsSlots = 31;

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
        ResetGameProgress();
        SetPlayerPrefsClass();

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

    public void SetPlayerPrefsClass()
    {
        PlayerPrefs.SetInt("playerClass", classSelection.value);
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

    public void NewGamePlayerPrefs()
    {
        PlayerPrefs.SetString("playerInvSlot_0_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_0_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_1_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_1_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_2_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_2_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_3_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_3_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_4_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_4_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_5_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_5_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_6_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_6_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_7_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_7_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_8_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_8_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_9_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_9_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_10_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_10_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_11_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_11_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_12_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_12_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_13_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_13_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_14_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_14_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_15_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_15_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_16_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_16_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_17_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_17_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_18_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_18_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_19_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_19_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_20_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_20_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_21_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_21_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_22_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_22_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_23_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_23_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_24_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_24_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_25_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_25_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_26_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_26_Item_stacks", 0);
        PlayerPrefs.SetString("playerInvSlot_27_Item", "");
        PlayerPrefs.SetInt("playerInvSlot_27_Item_stacks", 0);
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
        PlayerPrefs.SetInt("playerClass", 0);
        PlayerPrefs.SetInt("CurrentGold", 0);
        PlayerPrefs.SetInt("continue", 0);
    }
}