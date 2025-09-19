using System.Collections;
using UnityEngine;

public class CharacterAnimatorDelegate : MonoBehaviour
{
    [Header("Attack Points")]
    public GameObject leftArmAttackPoint, rightArmAttackPoint, leftLegAttackPoint, rightLegAttackPoint;

    [Header("Stand Up Settings")]
    public float standUpTimer = 2f;

    private CharacterAnimation animationScript;
    private HealthSystem healthSystem;
    private EnemyMovement enemyMovement;

    void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        healthSystem = GetComponent<HealthSystem>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // ---------------- ATAQUES ----------------
    void leftArmAttack_On() => leftArmAttackPoint.SetActive(true);
    void leftArmAttack_Off() { if (leftArmAttackPoint.activeInHierarchy) leftArmAttackPoint.SetActive(false); }

    void rightArmAttack_On() => rightArmAttackPoint.SetActive(true);
    void rightArmAttack_Off() { if (rightArmAttackPoint.activeInHierarchy) rightArmAttackPoint.SetActive(false); }

    void leftLegAttack_On() => leftLegAttackPoint.SetActive(true);
    void leftLegAttack_Off() { if (leftLegAttackPoint.activeInHierarchy) leftLegAttackPoint.SetActive(false); }

    void rightLegAttack_On() => rightLegAttackPoint.SetActive(true);
    void rightLegAttack_Off() { if (rightLegAttackPoint.activeInHierarchy) rightLegAttackPoint.SetActive(false); }

    // ---------------- LEVANTARSE ----------------
    void EnemyStandUp()
    {
        StartCoroutine(StandUpAfterTime());
    }

    IEnumerator StandUpAfterTime()
    {
        yield return new WaitForSeconds(standUpTimer);

        // Animación de levantarse
        animationScript.StandUp();

        // Restaurar vida y slider
        if (healthSystem != null)
        {
            healthSystem.InitializeHealth();
        }

        // Reactivar movimiento si estaba desactivado
        if (enemyMovement != null)
        {
            enemyMovement.enabled = true;
        }
    }
}
