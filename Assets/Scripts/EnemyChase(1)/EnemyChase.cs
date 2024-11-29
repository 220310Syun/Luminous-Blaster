using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform player; // �v���C���[��Transform
    [SerializeField] private float chaseSpeed = 2f; // �ǔ����x

    private void Start()
    {
        // �v���C���[���^�O�Ŏ擾
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player tag not found in the scene.");
        }
    }

    private void Update()
    {
        if (player == null) return; // �v���C���[�����Ȃ��ꍇ�͉������Ȃ�

        // �v���C���[�����ւ̐��m�ȃx�N�g�����v�Z
        Vector3 direction = (player.position - transform.position).normalized;

        // �ړ�
        transform.position += direction * chaseSpeed * Time.deltaTime;

        // �������v���C���[�ɍ��킹��
        FacePlayer();
    }

    private void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); // �E����
        }
        else if (player.position.x < transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x); // ������
        }
        transform.localScale = scale; // �X�P�[�����X�V
    }
}
