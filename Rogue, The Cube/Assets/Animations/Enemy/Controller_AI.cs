using UnityEngine;

public enum EnemyType { OFFENSIVE, DEFENSIVE }

public class Controller_AI : MonoBehaviour
{
    public Animator animator;

    private float detectionSphereRadius;
    public LayerMask targetLayer;
    [HideInInspector] public float attackingDistance;
    private EnemyType enemyType;

    public GameObject mainTarget;
    [HideInInspector] public bool staticEnemy;

    private float staminaEverySeconds;
    private int staminGainAmmount;
    private float _timerStaminaGain;

    private float manaEverySeconds;
    private int manaGainAmmount;
    private float _timerManaGain;

    private Stats_Enemy enemyStats; // for maybe prioritising the most hurt enemy. expand later
    [HideInInspector] public bool isEngaged = false;

    public void SetValues(Enemy enemy)
    {
        detectionSphereRadius = enemy.detectionSphereRadius;
        attackingDistance = enemy.attackingDistance;
        enemyType = enemy.enemyType;
        staminaEverySeconds = enemy.staminaEverySeconds;
        staminGainAmmount = enemy.staminGainAmmount;
        manaEverySeconds = enemy.manaEverySeconds;
        manaGainAmmount = enemy.manaGainAmmount;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyStats = GetComponent<Stats_Enemy>();
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
                        if (col.gameObject.CompareTag("Player") && !GetComponentInChildren<Controller_Character_Body_Invulnerability>().gotHit)
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
                            if (col.gameObject.GetComponentInParent<Stats>().CurrentHealth < col.gameObject.GetComponentInParent<Stats>().HITPOINTS.GetValue())
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
        else if (hitColliders.Length <= 0 && !isEngaged)
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

    public void AlertNearby(string target)
    {
        mainTarget = GameObject.FindGameObjectWithTag(target);

        animator.SetTrigger("isAttacking");
        isEngaged = true;

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, detectionSphereRadius, targetLayer);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    collider.gameObject.GetComponentInParent<Controller_AI>().mainTarget = mainTarget;
                    collider.gameObject.GetComponentInParent<Controller_AI>().animator.SetTrigger("isAttacking");
                }
            }
        }
    }
}