using UnityEngine;
using UnityEngine.UI;

public class EnemyResourcePanel : MonoBehaviour
{
    public Slider hpSlider;
    public Text enemyName;

    public Stats_Enemy[] stats_Enemy;
    private CanvasGroup cg;

    public GameObject enemyHolder;

    private float timer = 0f;
    private float coolDown = 2f;

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
        }
    }

    private void AdjustValues(int currentHealth, int maxHealth, string name)
    {
        cg.alpha = .6f;
        hpSlider.minValue = 0f;
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
        enemyName.text = name;
        timer = Time.time + coolDown;
    }

    private void Update()
    {
        if (cg.alpha >= 0f && Time.time > timer)
            cg.alpha -= 0.2f * Time.deltaTime;
    }
}
