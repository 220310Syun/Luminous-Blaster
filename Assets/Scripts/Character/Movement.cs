using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // ���̃X�N���v�g��Boss�ӊO�̑S�ẴL�����̈ړ������� (Player��Enemy��)
    //��邱��
    // �ϐ��̍쐬(�e���̈ړ��X�s�[�h�A �ړ���)

    [SerializeField]
    protected float xSpeed = 1.5f, ySpeed = 1.5f;
    private Vector2 moveDelta;


    private bool _hasPlayerTarget;
    public bool HasPlayerTarget
    {
        get { return _hasPlayerTarget; }
        set { _hasPlayerTarget = value; }
    }

    // x = HasPlayerTarget;


  // �֐��̍쐬 (�L�����N�^�[���ړ�������)
  protected void CharacterMovement(float x, float y)
    {
        moveDelta = new Vector2(x * xSpeed, y * ySpeed);

        transform.Translate(moveDelta.x * Time.deltaTime, moveDelta.y * Time.deltaTime, 0);
    }

    public Vector2 GetMoveDelta()
    {
        return moveDelta;
    }

}