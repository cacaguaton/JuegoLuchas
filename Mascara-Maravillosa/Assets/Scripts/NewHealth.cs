using UnityEngine;

public class NewHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool CharacterDied;
    public bool isPlayer;

    void Awake()
    {
        animationScript = GetComponentInChildren<CharacterAnimation>();
    }

    public void ApplyDamege(float damage, bool knockDown)
    {
        if (CharacterDied) return;
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            animationScript.Death();
            CharacterDied = true;

            if (!isPlayer)
            {
                if(knockDown)
                {
                    if(Random.Range(0, 2) > 0)
                    {
                        animationScript.KnokDown();
                    }
                    else
                    {
                        animationScript.Hit();
                    }
                }
            }
        }
    }
    
}
