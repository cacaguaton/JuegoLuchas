using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private CharacterAnimation enemyAnim;

    private Rigidbody myBody;
    public float speed = 5f;

    private Transform playerTarget;

    public float attack_Distance = 1f;
    private float chase_Player_After_Attack = 1;

    private float current_Attack_Time;
    private float default_Attack_Time = 2f;

    private bool followPlayer, attackPlayer;


    void Awake()
    {


        enemyAnim = GetComponentInChildren<CharacterAnimation>();
        myBody = GetComponent<Rigidbody>();

        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        followPlayer = true;
        current_Attack_Time = default_Attack_Time;
    }

    // Update is called once per frame
    void Update()
    {

        Attack();

    }

    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {


        //if we are not supposed to follow the player
        if (!followPlayer)
            return;

        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance)
        {
            transform.LookAt(playerTarget);
            myBody.linearVelocity = transform.forward * speed;

            if (myBody.linearVelocity.sqrMagnitude != 0)
            {
                enemyAnim.Walk(true);
            }
            else if (Vector3.Distance(transform.position, playerTarget.position) <= attack_Distance)
            {
                myBody.linearVelocity = Vector3.zero;
                enemyAnim.Walk(false);

                followPlayer = false;
                attackPlayer = true;

            }
        }

    }//Follow target

    void Attack()
    {

        //if we should NOT attack the player
        //exit the funtion
        if (!attackPlayer)
            return;

        current_Attack_Time += Time.deltaTime;

        if (current_Attack_Time > default_Attack_Time)
        {
            enemyAnim.EnemyAttack(Random.Range(0, 3));

            current_Attack_Time = 0f;
        }

        if (Vector3.Distance(transform.position, playerTarget.position) > attack_Distance +
        chase_Player_After_Attack)
        {
            attackPlayer = false;
            followPlayer = true;
        }

    }//attack


}// class

