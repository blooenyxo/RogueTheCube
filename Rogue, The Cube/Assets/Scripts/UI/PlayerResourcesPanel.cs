﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesPanel : MonoBehaviour
{
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider staminaSlider;

    public Text hpText;
    public Text mpText;
    public Text staminaText;

    public Stats_Player stats_Player;

    // Start is called before the first frame update
    void Start()
    {
        stats_Player = Stats_Player.instance;
        stats_Player.onResourcesChanged += AdjustValues;
        AdjustValues();
    }

    void AdjustValues()
    {
        hpSlider.maxValue = stats_Player.HITPOINTS.GetValue();
        hpSlider.minValue = 0f;

        mpSlider.maxValue = stats_Player.MANAPOINTS.GetValue();
        mpSlider.minValue = 0f;

        staminaSlider.maxValue = stats_Player.STAMINA.GetValue();
        staminaSlider.minValue = 0f;

        hpSlider.value = stats_Player.CurrentHealth;
        mpSlider.value = stats_Player.CurrentMana;
        staminaSlider.value = stats_Player.CurrentStamina;

        hpText.text = stats_Player.CurrentHealth.ToString() + " / " + stats_Player.HITPOINTS.GetValue();
        mpText.text = stats_Player.CurrentMana.ToString() + " / " + stats_Player.MANAPOINTS.GetValue();
        staminaText.text = stats_Player.CurrentStamina.ToString() + " / " + stats_Player.STAMINA.GetValue();
    }
}