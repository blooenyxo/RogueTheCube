using UnityEngine;

public class CloseButoons : MonoBehaviour
{
    [HideInInspector] public Player_Input_UI player_Input_UI;

    // this is not the way, but it works for now. think of removing this, once i learn how
    void Start()
    {
        if (player_Input_UI == null && GameObject.Find("Player"))
        {
            player_Input_UI = GameObject.Find("Player").GetComponent<Player_Input_UI>();
        }
    }

    public void InventoryPanel()
    {
        //player_Input_UI.SwitchInventoryState();
    }

    public void NPCPanel()
    {
        //player_Input_UI.SwitchNPCPanelState();
    }

    public void LootBoxPanel()
    {
        //player_Input_UI.SwitchLootBoxState();
    }
}