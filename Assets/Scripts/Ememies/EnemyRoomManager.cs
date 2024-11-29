using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    [SerializeField]
    private List<Movement> enemies = new List<Movement>(); // �G���X�g

    [SerializeField]
    private GameObject door;

    private void Start()
    {
        foreach (Transform tr in GetComponentInChildren<Transform>())
        {
            Movement enemy = tr.GetComponent<Movement>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
    }

    // �G�����X�g�ɒǉ����郁�\�b�h
    public void AddEnemy(Movement enemy)
    {
        if (enemy != null && !enemies.Contains(enemy)) // ���X�g�ɏd����h��
        {
            enemies.Add(enemy);
            Debug.Log($"Enemy {enemy.name} added to EnemyRoomManager.");
        }
    }

    public void EnablePlayerTarget()
    {
        foreach (Movement move in enemies)
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

    public void RemoveEnemy(Movement enemy)
    {
        enemies.Remove(enemy);
        CheckToUnlockGate();
    }

    private void CheckToUnlockGate()
    {
        if (enemies.Count == 0)
        {
            if (door)
            {
                door.SetActive(false);
            }
        }
    }
}
