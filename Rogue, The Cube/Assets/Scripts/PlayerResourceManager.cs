using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    private float _timerStaminaGain;
    public float staminaEverySeconds;
    public int staminGainAmmount;

    private float _timerManaGain;
    public float manaEverySeconds;
    public int manaGainAmmount;

    private float _timerHealthGain;
    public float healthEverySeconds;
    public int healthGainAmmount;

    void Update()
    {
        _timerStaminaGain += 1 * Time.deltaTime;
        if (_timerStaminaGain >= staminaEverySeconds)
        {
            GetComponent<Stats>().GainStamina(staminGainAmmount);
            _timerStaminaGain = 0f;
        }

        _timerManaGain += 1 * Time.deltaTime;
        if (_timerManaGain >= manaEverySeconds)
        {
            GetComponent<Stats>().GainMana(manaGainAmmount);
            _timerManaGain = 0f;
        }

        _timerHealthGain += 1 * Time.deltaTime;
        if (_timerHealthGain >= healthEverySeconds)
        {
            GetComponent<Stats>().Heal(healthGainAmmount);
            _timerHealthGain = 0f;
        }
    }
}