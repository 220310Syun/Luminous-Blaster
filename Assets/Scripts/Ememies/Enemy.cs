using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Movement
{

    //public bool Test;

    private Transform player;
    private Vector3 playerLastPos, startPos, movementPos;
    [SerializeField]
    private float chaseSpeed = 0.8f, turningDelay = 1f;
    private float lastFollowTime, turningTimeDelay = 1f;


    private Vector3 tempScalse;


    //攻撃力
    private bool attacked;
    [SerializeField]
    private float damageCooldown = 1f;
    private float damageCooldownTimer;

    [SerializeField]
    private int damageAmount = 1;


    private Health enemyHealth;


    private Animator animator;


    private EnemyRoomManager enemyRoomManager;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerLastPos = player.position;
        startPos = transform.position;
        lastFollowTime = Time.time;

        turningTimeDelay *= turningDelay;

        enemyHealth = GetComponent<Health>();

        animator = GetComponent<Animator>();

        enemyRoomManager = GetComponentInParent<EnemyRoomManager>();
    }


    private void OnDisable()
    {
        if (!enemyHealth.IsAllive()) 
        { 
           enemyRoomManager.RemoveEnemy(this);
        }
    }

    private void FixedUpdate()
    {
        if (!enemyHealth. IsAllive() || !player) 
        {
            return;
        }
        MoveAnimation();

        TurnAround();

        ChaseingPlayer();
    }


    void ChaseingPlayer()
    {
        if (HasPlayerTarget) 
        {

            if (!attacked)
            {
                Chase();
            }
            else
            {
                if (Time.time < damageCooldownTimer)//スペルミス変数ダブクリCtrl+R ×2 修正
                {
                    movementPos = startPos - transform.position;
                }
                else
                {
                    attacked = false;
                }
            }
            
        }
        else 
        {
            movementPos = startPos - transform.position;

            if (Vector3.Distance(transform.position, startPos) < 0.1f ) 
            {
                movementPos = Vector3.zero;
            }
        }

        CharacterMovement(movementPos.x, movementPos.y);
    }


    void Chase()
    {
        if (Time.time - lastFollowTime > turningTimeDelay) 
        {
            playerLastPos = player.transform.position;
            lastFollowTime = Time.time;
        }

    　　if (Vector3.Distance(transform.position, playerLastPos) > 0.15f)
        {
            movementPos = (playerLastPos - transform.position).normalized * chaseSpeed;
        }
        else
        {
            movementPos = Vector3.zero;
        }
    }


    void TurnAround()
    {
        tempScalse = transform.localScale;

        if (HasPlayerTarget)
        {
            if (player.position.x > transform.position.x)
            {
                tempScalse.x = Mathf.Abs(tempScalse.x);
            }
            if (player.position.x < transform.position.x) 
            {
                tempScalse.x = -Mathf.Abs(tempScalse.x);
            }
        }
        else
        {
            if (startPos.x > transform.position.x)
            {
                tempScalse.x = Mathf.Abs(tempScalse.x);
            }
            if (startPos.x < transform.position.x)
            {
                tempScalse.x = -Mathf.Abs(tempScalse.x);
            }
        }


        transform.localScale = tempScalse;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            damageCooldownTimer = Time.time + damageCooldown;

            attacked = true;

            collision.GetComponent<Health>().TakeDamage(damageAmount);
            collision.GetComponent<PlayerHealthUI>().SubtrackHealth(damageAmount);
        }
    }


    void MoveAnimation()
    {
        if (GetMoveDelta().sqrMagnitude > 0)
        {
            animator.SetBool("Walk",true);
        }
        else 
        {
            animator.SetBool("Walk",false);
        }
    }

}
