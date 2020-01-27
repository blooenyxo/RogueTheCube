using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesPanel : MonoBehaviour
{
    public Slider hpSlider;
    public Slider mpSlider;

    public Text hpText;
    public Text mpText;

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

        hpSlider.value = stats_Player.CurrentHealth;
        mpSlider.value = stats_Player.CurrentMana;

        hpText.text = stats_Player.CurrentHealth.ToString() + " / " + stats_Player.HITPOINTS.GetValue();
        mpText.text = stats_Player.CurrentMana.ToString() + " / " + stats_Player.MANAPOINTS.GetValue();
    }
}
