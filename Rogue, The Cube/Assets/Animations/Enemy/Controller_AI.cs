using UnityEngine;

public enum EnemyType { OFFENSIVE, DEFENSIVE }

public class Controller_AI : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float detectionSphereRadius;
    [SerializeField] private LayerMask targetLayer;

    [HideInInspector] public GameObject mainTarget;
    public float attackingDistance;
    public EnemyType enemyType;

    [Header("Stamina Gain")]
    public float staminaEverySeconds;
    public int staminGainAmmount;
    private float _timerStaminaGain;

    [Header("Mana Gain")]
    public float manaEverySeconds;
    public int manaGainAmmount;
    private float _timerManaGain;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionSphereRadius, targetLayer);
        if (hitColliders.Length > 0)
        {
            switch (enemyType)
            {
                case EnemyType.OFFENSIVE:
                    foreach (Collider col in hitColliders)
                    {
                        if (col.gameObject.CompareTag("Player"))
                        {
                            mainTarget = col.gameObject;
                            animator.SetTrigger("isAttacking");
                        }
                    }
                    break;
                case EnemyType.DEFENSIVE:
                    foreach (Collider col in hitColliders)
                    {
                        if (col.gameObject.CompareTag("Enemy"))
                        {
                            if (col.gameObject.GetComponent<Stats>().CurrentHealth < col.gameObject.GetComponent<Stats>().HITPOINTS.GetValue())
                            {
                                mainTarget = col.gameObject;
                                animator.SetTrigger("isHealing");
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else if (hitColliders.Length <= 0)
        {
            mainTarget = null;
        }

        #region Resource Management
        // resource management / gain back stamina and mana
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
        #endregion

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionSphereRadius);
    }
}
