using Unity.VisualScripting;
using UnityEngine;

public class UniversalAttack : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float attackRange = 0.5f;
    public int attackDamage = 10;

    public bool isPlayer, isEnemy;
    public GameObject hit_FX_prefab;


        void Update()
    {
        DetectCollision();
    }

    void DetectCollision()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, attackRange, collisionLayer);
        if (hit.Length > 0)
        {
            if (isPlayer)
            {
                Vector3 hitFX_Pos = hit[0].transform.position;
                hitFX_Pos.y += 1.3f;

                if (hit[0].transform.forward.x > 0)
                {
                    hitFX_Pos.x += 0.3f;
                }
                else if (hit[0].transform.forward.x < 0)
                {
                    hitFX_Pos.x -= 0.3f;
                }
                Instantiate(hit_FX_prefab, hitFX_Pos, Quaternion.identity);
                if (gameObject.CompareTag(Tags.LEFT_ARM_TAG) || gameObject.CompareTag(Tags.LEFT_LEG_TAG))
                {
                    hit[0].GetComponent<HealthSystem>().TakeDamage(attackDamage);
                }
                else
                {
                    hit[0].GetComponent<HealthSystem>().TakeDamage(attackDamage);
                }

            }//if is player

            if(isEnemy)
            {
                hit[0].GetComponent<HealthSystem>().TakeDamage(attackDamage, false);
            }

            print("Hit " + hit[0].gameObject.name);

            gameObject.SetActive(false);
        }
    }

}
