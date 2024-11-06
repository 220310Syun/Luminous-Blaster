using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField]
    private float deactivateTimer = 3f;

    private Rigidbody2D rb;

    [SerializeField]
    private int damageAmount = 3;

    private bool dealthDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("DeactivateBullet", deactivateTimer);
    }

    void DeactivateBullet()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Blocking"))
        {
            rb.velocity = Vector2.zero;
        }

        if (collision.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;

            if (!dealthDamage)
            {
                dealthDamage = true;
                collision.GetComponent<Health>().TakeDamage(damageAmount);

                collision.GetComponent<PlayerHealthUI>().SubtrackHealth(damageAmount);
            }
        }
    }

}
