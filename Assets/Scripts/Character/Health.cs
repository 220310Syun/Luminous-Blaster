using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{


    [SerializeField]
    private int maxHealth = 100;  //MaxHealthÇ5Ç©ÇÁ100Ç…ïœçX
    private int health;
    private Animator animator;

    public int PlayerHealth 
    { 
        get { return maxHealth; } 
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        health = maxHealth;
    }




    //ä÷êîÇÃçÏê¨

    public bool IsAllive()
    {
        return health > 0 ? true : false;
    }

    public void Destroy()
    {
        Destroy(gameObject); 
    }



    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount; 

        if (health < 0) 
        {
            animator.SetTrigger("Death");
        }
    }
}
