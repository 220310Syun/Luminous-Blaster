using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicSquareManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private MagicSquareManager[] playerMagicSquares;
    private int magicSquareIndex;

    [SerializeField]
    private GameObject[] magics;
    private Vector2 targetPos, direction, magicSpawnPos;
    private Camera cam;
    private Quaternion magicRotation;

    // Update is called once per frame
    private CameraShake cameraShake;
    [SerializeField]
    private float cameraShakeTimer = 0.2f;





    private void Awake()
    {
        magicSquareIndex = 0;
        playerMagicSquares[magicSquareIndex].gameObject.SetActive(true);

        cam = Camera.main;

        cameraShake = cam.GetComponent<CameraShake>();
    }

    private void Update()
    {
        ChangeMagic();
    }

    private void ChangeMagic()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            playerMagicSquares[magicSquareIndex].gameObject.SetActive(false);

            magicSquareIndex++;

            if (magicSquareIndex >= playerMagicSquares.Length)
            {
                magicSquareIndex = 0;
            }

            playerMagicSquares[magicSquareIndex ].gameObject.SetActive(true);

        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            playerMagicSquares[magicSquareIndex] .gameObject.SetActive(false);

            magicSquareIndex--;

            if (magicSquareIndex < 0)
            {
                magicSquareIndex = playerMagicSquares.Length - 1;
            }

            playerMagicSquares[magicSquareIndex].gameObject .SetActive(true);
        }



        for (int i = 0; i < playerMagicSquares.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                playerMagicSquares[magicSquareIndex].gameObject.SetActive(false) ;
                
                magicSquareIndex = i;

                playerMagicSquares[magicSquareIndex ] .gameObject.SetActive(true) ;

                break;
            }


        }

    }

    public void Activate(int dirIndex) 
    {
        playerMagicSquares[magicSquareIndex].ActivateMagicSquare(dirIndex);
    }



    public void Shoot(Vector2 spawnPos)
    {
         targetPos = cam.ScreenToWorldPoint(Input.mousePosition);

         magicSpawnPos = spawnPos;

        direction = (targetPos - magicSpawnPos).normalized;


        magicRotation = Quaternion.Euler(0,0,
             Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg);


        //GameObject newMagic = Instantiate(magics[magicSquareIndex],
        //    spawnPos,magicRotation);

        //newMagic.GetComponent<Magic>().MoveDirection(direction);
        //ctrl+K(“¯Žž‰Ÿ‚µ) +C

        MagicPool.Instance.Fire(magicSquareIndex,spawnPos
            ,magicRotation,direction);


        cameraShake.ShakeCamera(cameraShakeTimer);
    }
}
