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

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        SubscribeToEvents();
    }

    public void SubscribeToEvents()
    {
        stats_Enemy = enemyHolder.GetComponentsInChildren<Stats_Enemy>();
        foreach (Stats_Enemy se in stats_Enemy)
        {
            se.onEnemyHit += AdjustValues;
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

        for (int j = 0; j < enemyGameObject.GetComponent<Controller_Buffs>().buff.Length; j++)
        {
            if (enemyGameObject.GetComponent<Controller_Buffs>().buff[j] != null)
            {
                bool buffExists = false;
                for (int i = 0; i < BuffBackground.transform.childCount; i++)
                {
                    if (BuffBackground.transform.GetChild(i).GetComponent<EnemyResourcePanelBuff>().buff == enemyGameObject.GetComponent<Controller_Buffs>().buff[j])
                    {
                        //Debug.Log(enemyGameObject.GetComponent<Controller_Buffs>().buff[j].duration);
                        //Debug.Log(enemyGameObject.GetComponent<Controller_Buffs>().buffDuration[j]);

                        if (enemyGameObject.GetComponent<Controller_Buffs>().buffDuration[j] == enemyGameObject.GetComponent<Controller_Buffs>().buff[j].duration)
                        {
                            BuffBackground.transform.GetChild(i).GetComponent<EnemyResourcePanelBuff>().timeRemainingImage.fillAmount = 1;
                        }
                        buffExists = true;
                    }
                }

                if (!buffExists)
                {
                    GameObject BuffIcon = Instantiate(EnemyResourcePanelBuff);
                    BuffIcon.GetComponent<EnemyResourcePanelBuff>().buff = enemyGameObject.GetComponent<Controller_Buffs>().buff[j];
                    BuffIcon.GetComponent<EnemyResourcePanelBuff>().buffDuration = enemyGameObject.GetComponent<Controller_Buffs>().buffDuration[j];
                    BuffIcon.GetComponent<Image>().sprite = enemyGameObject.GetComponent<Controller_Buffs>().buff[j].sprite;
                    BuffIcon.transform.SetParent(BuffBackground.transform);
                    BuffBackground.GetComponent<CanvasGroup>().alpha = panelAlpha;
                }
            }
        }
    }

    private void EmptyBuffList()
    {
        for (int i = 0; i < BuffBackground.transform.childCount; i++)
        {
            Destroy(BuffBackground.transform.GetChild(i).gameObject);
            BuffBackground.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    private void Update()
    {
        if (cg.alpha >= 0f && Time.time > timer)
            cg.alpha -= 0.2f * Time.deltaTime;
    }
}
