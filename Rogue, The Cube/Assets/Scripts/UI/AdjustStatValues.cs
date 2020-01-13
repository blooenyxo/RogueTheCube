using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjustStatValues : MonoBehaviour
{

    public Text strValue;
    public Text agiValue;
    public Text intValue;

    Stats_Player stats_player;

    void Start()
    {
        stats_player = Stats_Player.instance;
        stats_player.onStatsChanged += AdjustValues;
    }

    void AdjustValues()
    {
        strValue.text = stats_player.STRENGHT.GetValue().ToString();
        agiValue.text = stats_player.AGILITY.GetValue().ToString();
        intValue.text = stats_player.INTELIGENCE.GetValue().ToString();
    }

}