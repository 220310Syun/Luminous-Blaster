using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    [SerializeField]
    private List<Movement> enemies = new List<Movement>(); // 敵リスト

    // ドアをリストで管理
    [SerializeField]
    private List<GameObject> doors; // Doors1, Doors2, Doors3

    // 各ドアを解除するための敵撃破数
    [SerializeField]
    private List<int> doorUnlockCounts; // [5, 10, 20]など撃破数に応じた条件を設定

    private int defeatedEnemies = 0; // 倒した敵の数

    private void Start()
    {
        // 敵をリストに追加
        foreach (Transform tr in GetComponentsInChildren<Transform>())
        {
            Movement enemy = tr.GetComponent<Movement>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }

        // ドアリストと解除条件が一致するか確認
        if (doors.Count != doorUnlockCounts.Count)
        {
            Debug.LogError("ドアリストと解除条件の数が一致していません！");
        }
    }

    public void AddEnemy(Movement enemy)
    {
        if (enemy != null && !enemies.Contains(enemy)) // リストに重複を防ぐ
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
        defeatedEnemies++; // 敵を倒したカウントを増やす

        Debug.Log($"Enemy defeated! Total: {defeatedEnemies}");

        CheckToUnlockDoors();
    }

    // 敵を倒した数に応じてドアを解除
    private void CheckToUnlockDoors()
    {
        for (int i = 0; i < doors.Count; i++)
        {
            if (defeatedEnemies >= doorUnlockCounts[i] && doors[i] != null && doors[i].activeSelf)
            {
                doors[i].SetActive(false); // ドアを解除
                Debug.Log($"Door {i + 1} unlocked!");
            }
        }
    }
}
