using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    [SerializeField]
    private List<Movement> enemies = new List<Movement>(); // �G���X�g

    // �h�A�����X�g�ŊǗ�
    [SerializeField]
    private List<GameObject> doors; // Doors1, Doors2, Doors3

    // �e�h�A���������邽�߂̓G���j��
    [SerializeField]
    private List<int> doorUnlockCounts; // [5, 10, 20]�Ȃǌ��j���ɉ�����������ݒ�

    private int defeatedEnemies = 0; // �|�����G�̐�

    private void Start()
    {
        // �G�����X�g�ɒǉ�
        foreach (Transform tr in GetComponentsInChildren<Transform>())
        {
            Movement enemy = tr.GetComponent<Movement>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }

        // �h�A���X�g�Ɖ�����������v���邩�m�F
        if (doors.Count != doorUnlockCounts.Count)
        {
            Debug.LogError("�h�A���X�g�Ɖ��������̐�����v���Ă��܂���I");
        }
    }

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
        defeatedEnemies++; // �G��|�����J�E���g�𑝂₷

        Debug.Log($"Enemy defeated! Total: {defeatedEnemies}");

        CheckToUnlockDoors();
    }

    // �G��|�������ɉ����ăh�A������
    private void CheckToUnlockDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (defeatedEnemies >= doorUnlockCounts[i] && doors[i] != null && doors[i].activeSelf)
            {
                doors[i].SetActive(false); // �h�A������
                Debug.Log($"Door {i + 1} unlocked!");
            }
        }
    }
}
