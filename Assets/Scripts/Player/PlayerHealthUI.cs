using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{

    [SerializeField]
    private GameObject[] healthHearts;

    private int currentHealthIndex,health;

    private Health playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<Health>();

        health = playerHealth.PlayerHealth;

        currentHealthIndex = health - 1;
    }



    public void SubtrackHealth(int damageAmount)
    {
        for (int i = 0; i < damageAmount; i++)
        {
            healthHearts[currentHealthIndex].SetActive(false);

            currentHealthIndex--;
            health--;

            if (health <= 0)
            {

                GameManager.Instance.GameOver();
                break;
            }

        }
    }
}
