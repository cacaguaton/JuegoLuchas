using UnityEngine;
 
public class EnemyMovement : MonoBehaviour
{
    private CharacterAnimation enemyAnim;
    private Rigidbody myBody;
    private Transform playerTarget;
    public float speed = 5f;
    public float attack_Distance = 5f;
    private float chase_Player_After_Attack = 3f;
    private float current_Attack_Time;
    private float default_Attack_Time = 2f;
    private bool followPlayer, attackPlayer;
 
    void Awake()
    {
        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();
        
        // Buscar al jugador pero de forma segura
        GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        if (player != null)
        {
            playerTarget = player.transform;
        }
    }
 
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }
 
private float searchPlayerInterval = 1f;
private float searchTimer = 0f;
 
void Update()
{
    Attack();
    
    // Buscar jugador solo cada cierto tiempo (optimización)
    searchTimer += Time.deltaTime;
    if (searchTimer >= searchPlayerInterval)
    {
        searchTimer = 0f;
        
        if (playerTarget == null)
        {
            GameObject player = GameObject.FindWithTag(Tags.PLAYER_TAG);
            if (player != null)
            {
                playerTarget = player.transform;
                followPlayer = true;
            }
            else
            {
                followPlayer = false;
                attackPlayer = false;
                enemyAnim.Walk(false);
            }
        }
    }
}
 
    void FixedUpdate()
    {
        FollowTarget();
    }
 
    void FollowTarget()
    {
        // Si no hay jugador o no debemos seguirlo, salir
        if (playerTarget == null || !followPlayer)
            return;
 
        float distance = Vector3.Distance(transform.position, playerTarget.position);
        
        if (distance > attack_Distance)
        {
            transform.LookAt(playerTarget);
            myBody.linearVelocity = transform.forward * speed;
            
            if (myBody.linearVelocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
        }
        else
        {
            // Detener el movimiento para atacar
            myBody.linearVelocity = Vector3.zero;
            enemyAnim.Walk(false);
            followPlayer = false;
            attackPlayer = true;
        }
    }
 
    void Attack()
    {
        // Si no hay jugador o no debemos atacar, salir
        if (playerTarget == null || !attackPlayer)
            return;
 
        current_Attack_Time += Time.deltaTime;
        
        if (current_Attack_Time > default_Attack_Time)
        {
            int randomAttack = Random.Range(0, 2);
            enemyAnim.EnemyAttack(randomAttack);
            Debug.Log("EjecutandoAtaque:" + randomAttack);
            current_Attack_Time = 0f;
        }
 
        // Verificar distancia solo si el jugador existe
        if (playerTarget != null)
        {
            float distance = Vector3.Distance(transform.position, playerTarget.position);
            if (distance > attack_Distance + chase_Player_After_Attack)
            {
                attackPlayer = false;
                followPlayer = true;
            }
        }
    }
}