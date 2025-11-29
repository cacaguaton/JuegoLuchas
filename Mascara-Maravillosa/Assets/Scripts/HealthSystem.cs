using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 100f;
    public float _currentHealth;
    private bool _isDead;

    [Header("UI")]
    [SerializeField] private Slider _healthSlider;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> _onTakeDamage;
    [SerializeField] private UnityEvent _onDie;

    [Header("Character Settings")]
    [SerializeField] private bool _isPlayer;
    private CharacterAnimation _animationScript;
    private EnemyMovement _enemyMovement;

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
}
