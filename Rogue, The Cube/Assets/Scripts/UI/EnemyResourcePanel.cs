using UnityEngine;
using UnityEngine.UI;

public class EnemyResourcePanel : MonoBehaviour
{
    public Slider hpSlider;
    public Text enemyName;

    public Stats_Enemy[] stats_Enemy;
    private CanvasGroup cg;

    public Controller_Buffs[] controller_Buffs;

    public GameObject enemyHolder;

    private float timer = 0f;
    private readonly float coolDown = 2f;
    private float panelAlpha = 1f;

    public GameObject EnemyResourcePanelBuff;
    public GameObject BuffBackground;
    public Transform[] BuffBackgroundSlots;

    public Gradient hpGradient;
    public Image hpBar;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;


        BuffBackgroundSlots = new Transform[BuffBackground.transform.childCount];
        for (int i = 0; i < BuffBackground.transform.childCount; i++)
        {
            BuffBackgroundSlots[i] = BuffBackground.transform.GetChild(i);
        }

        SubscribeToEvents();
    }

    public void SubscribeToEvents()
    {
        stats_Enemy = enemyHolder.GetComponentsInChildren<Stats_Enemy>();
        foreach (Stats_Enemy se in stats_Enemy)
        {
            se.onEnemyHealthChange += AdjustValues;
            se.onEnemyDeath += EmptyBuffList;
        }
    }

    private void AdjustValues(int currentHealth, int maxHealth, GameObject enemyGameObject)
    {
        cg.alpha = panelAlpha;
        hpSlider.minValue = 0f;
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
        enemyName.text = enemyGameObject.tag;
        timer = Time.time + coolDown;
        hpBar.color = hpGradient.Evaluate(hpSlider.normalizedValue);

        for (int j = 0; j < enemyGameObject.GetComponent<Controller_Buffs>().buff.Length; j++)
        {
            if (enemyGameObject.GetComponent<Controller_Buffs>().buff[j] != null)
            {
                bool buffExists = false;
                for (int i = 0; i < BuffBackground.transform.childCount; i++)
                {
                    if (BuffBackground.transform.GetChild(i).GetComponentInChildren<EnemyResourcePanelBuff>())
                    {
                        if (BuffBackground.transform.GetChild(i).GetComponentInChildren<EnemyResourcePanelBuff>().buff == enemyGameObject.GetComponent<Controller_Buffs>().buff[j])
                        {
                            //Debug.Log(enemyGameObject.GetComponent<Controller_Buffs>().buff[j].duration);
                            //Debug.Log(enemyGameObject.GetComponent<Controller_Buffs>().buffDuration[j]);

                            if (enemyGameObject.GetComponent<Controller_Buffs>().buffDuration[j] == enemyGameObject.GetComponent<Controller_Buffs>().buff[j].duration)
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
                    BuffIcon.GetComponent<EnemyResourcePanelBuff>().buff = enemyGameObject.GetComponent<Controller_Buffs>().buff[j];
                    BuffIcon.GetComponent<EnemyResourcePanelBuff>().buffDuration = enemyGameObject.GetComponent<Controller_Buffs>().buffDuration[j];
                    BuffIcon.GetComponent<Image>().sprite = enemyGameObject.GetComponent<Controller_Buffs>().buff[j].sprite;
                    BuffBackground.GetComponent<CanvasGroup>().alpha = panelAlpha;

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

    private void EmptyBuffList()
    {
        for (int i = 0; i < BuffBackgroundSlots.Length; i++)
        {
            if (BuffBackgroundSlots[i].childCount > 0)
            {
                Destroy(BuffBackgroundSlots[i].GetChild(0).gameObject);
                BuffBackground.GetComponent<CanvasGroup>().alpha = 0f;
            }
        }
    }

    private void Update()
    {
        if (cg.alpha >= 0f && Time.time > timer)
            cg.alpha -= 0.2f * Time.deltaTime;
    }
}
