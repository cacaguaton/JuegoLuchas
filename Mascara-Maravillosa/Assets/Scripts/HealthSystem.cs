using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 100f;
    public float _currentHealth;
    private bool _isDead;

    [Header("Health Settings")]
    [SerializeField]
    public float recuperacion = 30f;

    [Header("UI")]
    [SerializeField] private Slider _healthSlider;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onTakeDamage;
    [SerializeField] private UnityEvent _onDie;

    [Header("Character Settings")]
    [SerializeField] private bool _isPlayer;
    private CharacterAnimation _animationScript;
    private EnemyMovement _enemyMovement;


    public float tiempo = 3f;

    void Awake()
    {
        _animationScript = GetComponentInChildren<CharacterAnimation>();
        _enemyMovement = GetComponent<EnemyMovement>();
        InitializeHealth();
    }

    public void TakeDamage(float damage, bool knockDown = false)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        _onTakeDamage?.Invoke(damage);
        UpdateHealthSlider();

        if (_currentHealth <= 0)
        {
            Die();
            _animationScript.Death();
            _isDead= true;
             if(_isPlayer)
            {
                GameObject.FindWithTag(Tags.ENEMY_TAG)
                .GetComponent<EnemyMovement>().enabled = false;
            }

            return;
           
        }

        // Si no es jugador, se aplican animaciones de daño/knockdown
        if (!_isPlayer)
        {
            if (knockDown)
            {
                if (Random.Range(0, 2) > 0)
                {
                    _animationScript.KnokDown();
                }
                else
                {
                    _animationScript.Hit();
                }
            }
            else
            {
                _animationScript.Hit();
            }
        }
    }

    private void Die()
    {
        _isDead = true;
        _currentHealth = 0;

        _animationScript.Death();
        _onDie?.Invoke();

        UpdateHealthSlider();

        if (_enemyMovement != null)
    {
        _enemyMovement.enabled = false;
    }

    // 🚫 También puedes desactivar el collider si ya no quieres que reciba más golpes
    Collider col = GetComponent<Collider>();
    if (col != null)
    {
        col.enabled = false;
    }

    Rigidbody rigi = GetComponent<Rigidbody>();
    if (rigi != null)
    {
        rigi.isKinematic = true;
        //rigi.detectCollisions = false;
    }



    Destroy(gameObject, 5f);

    }

    public void InitializeHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealthSlider();
    }

    private void UpdateHealthSlider()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = _currentHealth / _maxHealth;
        }

    }




    public void RegenerateHealth()
    {

        if(_isPlayer && _currentHealth <= 99f)
        {

            if(_currentHealth <= 100f - recuperacion)
            {

                Debug.Log("Recupero 25 de vida");

                _currentHealth = _currentHealth + recuperacion;
                UpdateHealthSlider();
                Destroy(gameObject);
                return;
                

            }
            else
            {
            
            Debug.Log("Recupero incompleta"); 

            _currentHealth = 100f;
            UpdateHealthSlider();
            Destroy(gameObject);
            return;


            }


            //Debug.Log("Se activo Recuperacion");
        }
        else
        {
            Debug.Log("No se puede agarrar");
        }


    }


    IEnumerator EsperarYActivar()
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(tiempo);

        // Llama a la función deseada
        
    }
}
