using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{


    [SerializeField]
    private float walkSpeed = 0.5f, runSpeed = 1f;
    private float moveSpeed;

    [SerializeField]
    private Transform[] movePositons;

    private bool playerDetected, chasePlayer;
    private Transform playerPos;

    private Health health;

    private Vector3 targetPos;


    private Vector3 tempScale;

    [SerializeField]
    private int damageAmount = 5;

    [SerializeField]
    private float shootTimeDelay = 2f;
    private float shootTimer;

    private EnemyShoot enemyShoot;


    private void Start()
    {
        moveSpeed = walkSpeed;

        RandomMovePositon();

        playerPos = GameObject.FindWithTag("Player").transform;
        health = GetComponent<Health>();

        enemyShoot = GetComponent<EnemyShoot>();
    }


    private void FixedUpdate()
    {
        if (!playerPos || !health.IsAllive())
        {
            return;
        }

        FaceDirection();

        Movement();

        Shooting();
    }


    void RandomMovePositon()
    {
        Debug.Log("aaa");
        int randomIndex = Random.Range(0, movePositons.Length);

        while (targetPos == movePositons[randomIndex].position)
        {
            randomIndex = Random.Range(0, movePositons.Length);
        }

        targetPos = movePositons[randomIndex].position;
    }


    void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            if (playerDetected)
            {
                targetPos = playerPos.position;
                chasePlayer = true;
            }
            else
            {
                if (!chasePlayer)
                {
                    RandomMovePositon();
                }

            }
        }
    }


    void ChangeMoveSpeed(bool detected)
    {
        if (detected)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }


    public void PlayerDetected(bool detected)
    {
        playerDetected = detected;

        ChangeMoveSpeed(detected);

        if (!playerDetected)
        {
            chasePlayer = false;
            RandomMovePositon();
        }
    }


    void FaceDirection()
    {
        tempScale = transform.localScale;

        if (targetPos.x > transform.position.x)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (targetPos.x < transform.position.x)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
        }

        transform.localScale = tempScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasePlayer = false;
            RandomMovePositon() ;

            collision.GetComponent<Health>().TakeDamage(damageAmount);
            collision.GetComponent<PlayerHealthUI>().SubtrackHealth(damageAmount);
        }
    }


    void Shooting()
    {
        if (playerDetected)
        {
            if (Time.time > shootTimer)
            {
                shootTimer = Time.time + shootTimeDelay;

                Vector2 direction = (playerPos.position - transform.position).normalized;
                enemyShoot.Shoot(direction,transform.position);
            }
        }
    }


    private void OnDisable()
    {
        if (!health.IsAllive())
        {
            GameManager.Instance.DelayGameClear();
        }
    }
}


