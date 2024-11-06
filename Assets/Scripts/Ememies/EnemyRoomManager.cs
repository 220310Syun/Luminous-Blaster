using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{

    [SerializeField]
    private List<Movement> enemies;


    private void Start()
    {
        foreach(Transform tr in GetComponentInChildren<Transform>())
        {
            enemies.Add(tr.GetComponent<Movement>());
        }
    }



    public void EnablePlayerTarget()
    {
        foreach(Movement move in enemies) 
        {
            move.HasPlayerTarget = true;
        }
    }


    public void DisablePlayerTarget()
    {
        foreach (Movement move in enemies)
        {
            move.HasPlayerTarget = false;
        }
    }
}
