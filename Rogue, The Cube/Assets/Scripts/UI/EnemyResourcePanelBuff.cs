using UnityEngine;
using UnityEngine.UI;

public class EnemyResourcePanelBuff : MonoBehaviour
{
    public Buff buff;
    public float buffDuration;
    public Text buffName;
    public Image timeRemainingImage;

    private void Start()
    {
        buffName.text = buff.title;
    }

    private void Update()
    {
        timeRemainingImage.fillAmount -= 1 / buffDuration * Time.deltaTime;
        if (timeRemainingImage.fillAmount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
