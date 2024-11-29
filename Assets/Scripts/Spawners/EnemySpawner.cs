using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // 敵のPrefab
    [SerializeField] private float spawnInterval = 5f; // スポーン間隔

    private List<Transform> spawnAreas = new List<Transform>(); // 子オブジェクトのリスト

    private void Start()
    {
        // 子オブジェクトを取得してリストに追加
        foreach (Transform child in transform)
        {
            spawnAreas.Add(child);
        }

        // スポーン開始
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // ランダムなスポーンエリアを選択
            Transform randomSpawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            BoxCollider2D collider = randomSpawnArea.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                Debug.LogError($"No BoxCollider2D found on {randomSpawnArea.name}");
                continue;
            }

            Bounds bounds = collider.bounds;

            // ランダムな位置で敵をスポーン
            Vector3 spawnPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                0f
            );

            // ランダムな敵を生成
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            Debug.Log($"Spawned enemy {enemy.name} at position {spawnPosition}");

            // EnemyRoomManager に登録
            EnemyRoomManager roomManager = FindObjectOfType<EnemyRoomManager>();
            if (roomManager != null)
            {
                roomManager.AddEnemy(enemy.GetComponent<Movement>());
                Debug.Log($"Enemy {enemy.name} added to EnemyRoomManager.");
            }

            // 初期状態で追尾を無効化
            Movement movement = enemy.GetComponent<Movement>();
            if (movement != null)
            {
                movement.HasPlayerTarget = false;
            }
            else
            {
                Debug.LogError($"Spawned enemy {enemy.name} does not have a Movement component.");
            }
        }
    }
}
