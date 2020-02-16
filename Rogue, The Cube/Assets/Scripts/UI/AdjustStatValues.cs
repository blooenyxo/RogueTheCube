using UnityEngine;
using UnityEngine.UI;

public class AdjustStatValues : MonoBehaviour
{
    public Text strValue;
    public Text agiValue;
    public Text intValue;
    public Text dmgValue;
    public Text magValue;
    public Text goldValue;

    Stats_Player stats_player;

    void Start()
    {
        if (Stats_Player.instance)
        {
            stats_player = Stats_Player.instance;
            stats_player.onStatsChanged += AdjustValues;
            stats_player.onPlayerDeath += Reset;
        }
    }

    private void LateUpdate()
    {
        if (stats_player == null && Stats_Player.instance)
        {
            stats_player = Stats_Player.instance;
            stats_player.onStatsChanged += AdjustValues;
            stats_player.onPlayerDeath += Reset;
        }
    }

    void AdjustValues()
    {
        strValue.text = stats_player.STRENGHT.GetValue().ToString();
        agiValue.text = stats_player.AGILITY.GetValue().ToString();
        intValue.text = stats_player.INTELIGENCE.GetValue().ToString();
        dmgValue.text = Mathf.CeilToInt(stats_player.MINDMG.GetValue() + (stats_player.STRENGHT.GetValue() * .1f)).ToString() + " - " + Mathf.CeilToInt(stats_player.MAXDMG.GetValue() + (stats_player.STRENGHT.GetValue() * .1f)).ToString();
        magValue.text = Mathf.CeilToInt(stats_player.MINMAGIC.GetValue() + (stats_player.INTELIGENCE.GetValue() * .1f)).ToString() + " - " + Mathf.CeilToInt(stats_player.MAXMAGIC.GetValue() + (stats_player.INTELIGENCE.GetValue() * .1f)).ToString();
        goldValue.text = stats_player.CurrentGold.ToString();
    }

    private void Reset()
    {
        stats_player = null;
    }
}