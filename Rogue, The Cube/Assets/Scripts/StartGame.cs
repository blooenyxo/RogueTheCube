using System.Collections;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public static StartGame instance;

    public GameObject player;
    public Transform spawnPoint;
    public bool playerExists = false;
    public Stats_Player stats_player;
    public CheatCodes cc;
    public GameObject _item_ui;
    public Transform _item_ui_spawnTransform;

    public Transform[] allSlots;
    public GameObject[] equipmentSlots;

    private GameObject equipmentBackground;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one Inventory found");
            return;
        }
        instance = this;

        CreatePlayer();
    }

    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    private void CreatePlayer()
    {
        GameObject p = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        p.transform.SetParent(spawnPoint);
        p.name = "Player";

        stats_player = Stats_Player.instance;
    }

    // Equip all UI_Items from the Equipment Slots 
    private void SetupPlayer()
    {
        for (int i = 28; i <= 31; i++)
        {
            if (allSlots[i].transform.childCount > 0)
            {
                allSlots[i].GetComponent<Item_Drop_Eq>().localStoredItem = allSlots[i].GetComponentInChildren<Item_UI>().item;
                allSlots[i].GetComponent<Item_Drop_Eq>().EquipItem(allSlots[i].GetChild(0).gameObject);
            }
        }
    }

    private void CreateItem(Item _item, Transform currentParent, int slotPosition)
    {
        GameObject createdItem = Instantiate(_item_ui, currentParent);
        createdItem.GetComponent<Item_UI>().item = _item;

        if (PlayerPrefs.GetInt("playerInvSlot" + slotPosition + "stacks", 0) != 0)
        {
            createdItem.GetComponent<Item_UI>().stacks = PlayerPrefs.GetInt("playerInvSlot" + slotPosition + "stacks", 0);
            createdItem.GetComponent<Item_UI>().AdjustStackText();
        }
    }

    public IEnumerator LoadGame()
    {
        // Set Class
        switch (PlayerPrefs.GetInt("PlayerClass", 0))
        {
            case 0:
                stats_player.STRENGHT.AddModifier(3);
                break;
            case 1:
                stats_player.AGILITY.AddModifier(3);
                break;
            case 2:
                stats_player.INTELIGENCE.AddModifier(3);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(.1f); //this wait is needed for the equipment to work

        // Set All the Items
        for (int i = 0; i <= SetupScene.nrPlayerPrefsSlots; i++)
        {
            if (PlayerPrefs.GetInt("playerInvSlot" + i + "state", 0) == 1)
            {
                CreateItem(ItemDistriburion.instance.AllItems().Find(x => x.ITEMNAME == PlayerPrefs.GetString("playerInvSlot" + i + "item")), allSlots[i], i);
            }
        }


        // Equip all the items currently on the equipment slots
        SetupPlayer();

        // Set hp and mp to max 
        stats_player.SetAllToMax();
        // Set Gold value
        stats_player.GainGold(PlayerPrefs.GetInt("CurrentGold", 0));
    }

    public void SaveGame()
    {
        for (int i = 0; i <= SetupScene.nrPlayerPrefsSlots; i++)
        {
            if (allSlots[i].childCount > 0)
            {
                PlayerPrefs.SetInt("playerInvSlot" + i + "state", 1);
                PlayerPrefs.SetString("playerInvSlot" + i + "item", allSlots[i].GetComponentInChildren<Item_UI>().item.ITEMNAME);
                PlayerPrefs.SetInt("playerInvSlot" + i + "stacks", allSlots[i].GetComponentInChildren<Item_UI>().stacks);
            }
            else if (allSlots[i].childCount == 0)
            {
                PlayerPrefs.SetInt("playerInvSlot" + i + "state", 0);
            }
        }

        PlayerPrefs.SetInt("CurrentGold", stats_player.CurrentGold);
    }
}