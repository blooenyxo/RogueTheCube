using UnityEngine;

public class CloseButoons : MonoBehaviour
{
    [HideInInspector] public Controller_Input ci_player;


    // this is not the way, but it works for now. think of removing this, once i learn how
    void LateUpdate()
    {
        if (ci_player == null && GameObject.Find("Player"))
        {
            ci_player = GameObject.Find("Player").GetComponent<Controller_Input>();
        }
    }

    public void InventoryPanel()
    {
        ci_player.SwitchInventoryState();
    }

    public void NPCPanel()
    {
        ci_player.SwitchNPCPanelState();
    }

    public void LootBoxPanel()
    {
        ci_player.SwitchLootBoxState();
    }
}