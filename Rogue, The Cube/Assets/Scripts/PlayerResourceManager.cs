using UnityEngine;

public class PlayerResourceManager : MonoBehaviour
{
    private float _timerStaminaGain;
    public float staminaEverySeconds;
    public int staminGainAmmount;

    void Update()
    {
        _timerStaminaGain += 1 * Time.deltaTime;
        if (_timerStaminaGain >= staminaEverySeconds)
        {
            GetComponent<Stats>().GainStamina(staminGainAmmount);
            _timerStaminaGain = 0f;
        }
    }
}