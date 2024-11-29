using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // �G��Prefab
    [SerializeField] private float spawnInterval = 5f; // �X�|�[���Ԋu

    private List<Transform> spawnAreas = new List<Transform>(); // �q�I�u�W�F�N�g�̃��X�g

    private void Start()
    {
        // �q�I�u�W�F�N�g���擾���ă��X�g�ɒǉ�
        foreach (Transform child in transform)
        {
            spawnAreas.Add(child);
        }

        // �X�|�[���J�n
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // �����_���ȃX�|�[���G���A��I��
            Transform randomSpawnArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            BoxCollider2D collider = randomSpawnArea.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                Debug.LogError($"No BoxCollider2D found on {randomSpawnArea.name}");
                continue;
            }

            Bounds bounds = collider.bounds;

            // �����_���Ȉʒu�œG���X�|�[��
            Vector3 spawnPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                0f
            );

            // �����_���ȓG�𐶐�
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            Debug.Log($"Spawned enemy {enemy.name} at position {spawnPosition}");

            // EnemyRoomManager �ɓo�^
            EnemyRoomManager roomManager = FindObjectOfType<EnemyRoomManager>();
            if (roomManager != null)
            {
                roomManager.AddEnemy(enemy.GetComponent<Movement>());
                Debug.Log($"Enemy {enemy.name} added to EnemyRoomManager.");
            }

            // ������ԂŒǔ��𖳌���
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
