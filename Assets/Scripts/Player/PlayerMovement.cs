using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class PlayerMovement : Movement
{
    // Start is called before the first frame update
    private float moveX, moveY;


    private Camera mainCam;
    private Vector2 mousePos, direction;
    private Vector3 tempScale;
    private Animator animator;



    private PlayerMagicSquareManager PlayerMagicSquareManager;


    private void Awake()
    {
        mainCam = Camera.main;
        animator = GetComponent<Animator>();

        PlayerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }



    private void FixedUpdate()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        PlayerTurning();

        CharacterMovement(moveX,moveY);

    }

    void PlayerTurning()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        direction = new Vector2(mousePos.x - transform.position.x,
            mousePos.y - transform.position.y).normalized;

        PlayerAnimation(direction.x,direction.y);
    }

    void PlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = transform.localScale;

        if (x > 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
        }
        else if (x < 0)
        {
            tempScale.x = -Mathf.Abs(tempScale.x);//-1* -1
        }

        transform.localScale = tempScale;

        x = Mathf.Abs(x);

        animator.SetFloat("FaceX", x);
        animator.SetFloat("FaceY", y);

        DirectionMagicSquare(x,y);

    }


    void DirectionMagicSquare(float x, float y)
    {
        if (x == 1f && y == 0)
        {
            PlayerMagicSquareManager.Activate(0);
        }
        if (x == 0f && y == 1f)
        {
            PlayerMagicSquareManager.Activate(1);
        }
        if (x == 0f && y == -1f)
        {
            PlayerMagicSquareManager.Activate(2);
        }
        //Side_Up
        if (x == 1f && y == 1f)
        {
            PlayerMagicSquareManager.Activate(3);
        }
        //Side_Down
        if (x == 1f && y == -1f)
        {
            PlayerMagicSquareManager.Activate(4);
        }
    }

}
