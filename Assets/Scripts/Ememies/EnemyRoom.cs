using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyTarget
{
    EnableTarget,
    DisableTarget,
}


public class EnemyRoom : MonoBehaviour
{


    [SerializeField]
    private EnemyTarget enemyTarget;

    [SerializeField]
    private EnemyRoomManager enemyRoomManager;

    [SerializeField]
    private BossMove boss;
    [SerializeField]
    private bool bossZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(bossZone)
        {
            if (collision.CompareTag("Player"))
            {
                if (boss && enemyTarget == EnemyTarget.EnableTarget)
                {
                    boss.PlayerDetected(true);
                }
                else if(boss && enemyTarget == EnemyTarget.DisableTarget)
                {
                    boss.PlayerDetected(false);
                }
            }

        }
        else
        {
            if (collision.CompareTag("Player"))
            {
              if (enemyTarget == EnemyTarget.EnableTarget)
              {
                enemyRoomManager.EnablePlayerTarget();
              }
              else
              {
                enemyRoomManager.DisablePlayerTarget();
              }
        }
        }

        
    }
}
