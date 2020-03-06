using System.Collections;
using UnityEngine;

public class startgametest : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;
    public bool playerExists = false;
    public Stats_Player stats_player;
    public CheatCodes cc;

    private void Update()
    {
        //if (!playerExists && Input.GetButton("Jump"))
        //{
        //    GameObject p = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        //    p.transform.SetParent(spawnPoint);
        //    p.name = "Player";
        //    playerExists = true;

        //    //SetupPlayer(p.GetComponent<Controller_Equipment>());
        //}

        //if (Stats_Player.instance && stats_player == null)
        //{
        //    stats_player = Stats_Player.instance;
        //    stats_player.onPlayerDeath += ResetPlayerExists;
        //}
    }

    private void Start()
    {
        CreatePlayer();
        SetPlayerStats();
        StartCoroutine(SetPlayerWeapons());
    }

    private void CreatePlayer()
    {
        GameObject p = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
        p.transform.SetParent(spawnPoint);
        p.name = "Player";

        stats_player = Stats_Player.instance;
        stats_player.onPlayerDeath += ResetPlayerExists;
    }

    private void SetPlayerStats()
    {
        switch (PlayerPrefs.GetInt("playerClass", 0))
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
    }

    private IEnumerator SetPlayerWeapons()
    {
        yield return new WaitForSeconds(.1f);

        switch (PlayerPrefs.GetInt("playerStartingWeapon", 0))
        {
            case 0:
                cc.Equip_STR_Items();
                break;
            case 1:
                cc.Equip_AGI_Items();
                break;
            case 2:
                cc.Equip_INT_Items();
                break;
            default:
                break;
        }

        stats_player.SetAllToMax();

    }


    private void SetupPlayer(Controller_Equipment eq)
    {
        GameObject equipmentBackground = GameObject.Find("EquipmentBackground");
        for (int i = 0; i < equipmentBackground.transform.childCount; i++)
        {
            if (equipmentBackground.transform.GetChild(i).childCount > 0)
            {
                eq.Equip(equipmentBackground.transform.GetChild(i).GetComponentInChildren<Item_UI>().item, i);
                eq.EquipItemGameObject(equipmentBackground.transform.GetChild(i).gameObject, i);
            }
        }
    }

    public void ResetPlayerExists()
    {
        playerExists = !playerExists;
    }
}