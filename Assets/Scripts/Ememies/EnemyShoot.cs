using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private int numberOfBullets = 3;

    [SerializeField]
    private float bulletSpeed = 1f;


    public void Shoot(Vector3 direction,Vector3 firePositon)
    {
        float offset = 0.5f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Quaternion rot = Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            GameObject bullet = Instantiate(enemyBullet, firePositon,rot);

            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            direction.x += Random.Range(-offset, offset);
            direction.y += Random.Range(-offset, offset);
        }

    }

}
