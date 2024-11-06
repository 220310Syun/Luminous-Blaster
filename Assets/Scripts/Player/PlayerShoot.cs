using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //ƒvƒŒƒCƒ„[‚ÌËŒ‚

    [SerializeField]
    private float shootTimer, shootTimeDelay = 0.2f;

    [SerializeField]
    private Transform magicSpawnPos;
    private PlayerMagicSquareManager playerMagicSquareManager;




    private void Awake()
    {
        playerMagicSquareManager = GetComponent<PlayerMagicSquareManager>();
    }


    private void Update()
    {
        Shooting();
    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.time > shootTimer) 
            {
                shootTimer = Time.time + shootTimeDelay;


                //ŠÖ”‚ğŒÄ‚Ô
                playerMagicSquareManager.Shoot(magicSpawnPos.position);
            }
        
       
        }


    }

}
