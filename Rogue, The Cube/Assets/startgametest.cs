using UnityEngine;

public class startgametest : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPoint;
    public bool playerExists = false;
    public Stats_Player stats_player;

    private void Update()
    {
        if (!playerExists && Input.GetButton("Jump"))
        {
            GameObject p = Instantiate(player, spawnPoint.position, spawnPoint.rotation);
            p.transform.SetParent(spawnPoint);
            p.name = "Player";
            playerExists = true;

            //SetupPlayer(p.GetComponent<Controller_Equipment>());
        }

        if (Stats_Player.instance && stats_player == null)
        {
            stats_player = Stats_Player.instance;
            stats_player.onPlayerDeath += ResetPlayerExists;
        }
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