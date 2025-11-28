using System.Collections;
using UnityEngine;

public class CharacterAnimatorDelegate : MonoBehaviour
{
    [Header("Attack Points")]
    public GameObject leftArmAttackPoint, rightArmAttackPoint, leftLegAttackPoint, rightLegAttackPoint;

    [Header("Stand Up Settings")]
    public float standUpTimer = 2f;

    private CharacterAnimation animationScript;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip whoosh_Sound, fall_Sound, grount_Hit_Sound, dead_Sound;

    private HealthSystem healthSystem;
    private EnemyMovement enemyMovement;

    private ShakeCamera shakeCamera;

    void Awake()
    {
        animationScript = GetComponent<CharacterAnimation>();
        healthSystem = GetComponent<HealthSystem>();
        enemyMovement = GetComponent<EnemyMovement>();

        audioSource = GetComponent<AudioSource>();

        if (gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            enemyMovement = GetComponentInParent<EnemyMovement>();
        }

        shakeCamera = GameObject.FindWithTag(Tags.MAIN_CAMERA_TAG).GetComponent<ShakeCamera>();
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

    
    /*
    void TagLeft_Arm() {leftArmAttackPoint.tag = Tags.LEFT_ARM_TAG; }

    void UnTagLeft_Arm() { leftArmAttackPoint.tag = Tags.UNTAGGED_TAG; }



     void TagLeft_Leg() {leftLegAttackPoint.tag = Tags.LEFT_LEG_TAG; }

    void UnTagLeft_Leg() { leftLegAttackPoint.tag = Tags.UNTAGGED_TAG; }*/

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


    //Acomodar en animaciones como eventos para cada sonido asi como el acivar o desactivar el movimiento

    public void Attack_FX_Sound()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = whoosh_Sound;
        audioSource.Play();

    }

    public void CharacterDiedSound()
    {
        
        audioSource.volume = 1f;
        audioSource.clip = dead_Sound;
        audioSource.Play();

    }

    void Enemy_KnockedDown()
    {
        audioSource.clip = fall_Sound;
        audioSource.Play();
    }
    
    void Enemy_HitGround()
    {
        audioSource.clip = grount_Hit_Sound;
        audioSource.Play();
    }

    void DisableMovement()
    {
        enemyMovement.enabled = false;
        //set the enemy parent to default layer
        transform.parent.gameObject.layer = 0;
    }

    void EnableMovement()
    {
        enemyMovement.enabled = true;
        //set the enemy parent to enemy layer
        transform.parent.gameObject.layer = 7;
    }

    void ShakeCameraOnFall()
    {
        
        shakeCamera.ShouldShake = true;

    }

    void CharacterDied()
    {
        Invoke("DesactivateGameObject", 2f);
    }
    
    void DesactivateGameObject()
    {
        //EnemyManager.instance.instance.SpawnEnemy();
        gameObject.SetActive(false);
    }
    
    }
