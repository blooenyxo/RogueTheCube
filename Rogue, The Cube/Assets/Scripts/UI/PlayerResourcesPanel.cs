using UnityEngine;
using UnityEngine.UI;

public class PlayerResourcesPanel : MonoBehaviour
{
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider staminaSlider;

    public Text hpText;
    public Text mpText;
    public Text staminaText;
    public Text goldText; // this works best as a resource not as a stat. keep it here, even if the gold text is child of equipment panel

    public Stats_Player stats_Player;

    public Gradient hpGradient;
    public Gradient mpGradient;

    public Image hpBar;
    public Image mpBar;

    public GameObject EnemyResourcePanelBuff;
    public GameObject BuffBackground;
    public Transform[] BuffBackgroundSlots;

    // Start is called before the first frame update
    void Start()
    {
        if (Stats_Player.instance)
        {
            stats_Player = Stats_Player.instance;
            stats_Player.onResourcesChanged += AdjustValues;

            AdjustValues();
        }
        else
        {
            GetComponent<CanvasGroup>().alpha = 0;
        }


        BuffBackgroundSlots = new Transform[BuffBackground.transform.childCount];
        for (int i = 0; i < BuffBackground.transform.childCount; i++)
        {
            BuffBackgroundSlots[i] = BuffBackground.transform.GetChild(i);
        }
    }

    private void LateUpdate()
    {
        if (stats_Player == null && Stats_Player.instance)
        {
            stats_Player = Stats_Player.instance;
            stats_Player.onResourcesChanged += AdjustValues;
            GetComponent<CanvasGroup>().alpha = 1;
        }
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

        hpBar.color = hpGradient.Evaluate(hpSlider.normalizedValue);
        mpBar.color = mpGradient.Evaluate(mpSlider.normalizedValue);

        goldText.text = stats_Player.CurrentGold.ToString();


        for (int j = 0; j < stats_Player.gameObject.GetComponent<Controller_Buffs>().buff.Length; j++)
        {
            if (stats_Player.gameObject.GetComponent<Controller_Buffs>().buff[j] != null)
            {
                bool buffExists = false;
                for (int i = 0; i < BuffBackground.transform.childCount; i++)
                {
                    if (BuffBackground.transform.GetChild(i).GetComponentInChildren<EnemyResourcePanelBuff>())
                    {
                        if (BuffBackground.transform.GetChild(i).GetComponentInChildren<EnemyResourcePanelBuff>().buff == stats_Player.gameObject.GetComponent<Controller_Buffs>().buff[j])
                        {
                            if (stats_Player.gameObject.GetComponent<Controller_Buffs>().buffDuration[j] == stats_Player.gameObject.GetComponent<Controller_Buffs>().buff[j].duration)
                            {
                                BuffBackground.transform.GetChild(i).GetComponentInChildren<EnemyResourcePanelBuff>().timeRemainingImage.fillAmount = 1;
                            }
                            buffExists = true;
                        }
                    }
                }

                if (!buffExists)
                {
                    GameObject BuffIcon = Instantiate(EnemyResourcePanelBuff, this.transform);
                    BuffIcon.GetComponent<EnemyResourcePanelBuff>().buff = stats_Player.gameObject.GetComponent<Controller_Buffs>().buff[j];
                    BuffIcon.GetComponent<EnemyResourcePanelBuff>().buffDuration = stats_Player.gameObject.GetComponent<Controller_Buffs>().buffDuration[j];
                    BuffIcon.GetComponent<Image>().sprite = stats_Player.gameObject.GetComponent<Controller_Buffs>().buff[j].sprite;
                    BuffBackground.GetComponent<CanvasGroup>().alpha = 1;

                    for (int i = 0; i < BuffBackgroundSlots.Length; i++)
                    {
                        if (BuffBackgroundSlots[i].childCount == 0)
                        {
                            BuffIcon.transform.SetParent(BuffBackgroundSlots[i]);
                            return;
                        }
                    }
                }
            }
        }
    }
}