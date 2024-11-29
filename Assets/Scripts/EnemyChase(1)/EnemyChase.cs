using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform player; // プレイヤーのTransform
    [SerializeField] private float chaseSpeed = 2f; // 追尾速度

    private void Start()
    {
        // プレイヤーをタグで取得
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player tag not found in the scene.");
        }
    }

    private void Update()
    {
        if (player == null) return; // プレイヤーがいない場合は何もしない

        // プレイヤー方向への正確なベクトルを計算
        Vector3 direction = (player.position - transform.position).normalized;

        // 移動
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // 向きをプレイヤーに合わせる
        FacePlayer();
    }

    private void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); // 右向き
        }
        else if (player.position.x < transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x); // 左向き
        }
        transform.localScale = scale; // スケールを更新
    }
}
