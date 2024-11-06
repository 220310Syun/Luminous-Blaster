using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float moveSpeed = 2.5f, deactiveTimer = 3f;
    [SerializeField]
    private int damageAmount = 25;
    private bool damage;
    [SerializeField]
    private bool destoryObj;
    [SerializeField]
    private bool getTrailRanderer;
    private TrailRenderer trail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (getTrailRanderer)
        {
            trail = GetComponent<TrailRenderer>();
        }
    }

    private void OnEnable()
    {
        damage = false;

        //ŠÖ”‚ğŒÄ‚Ô

        Invoke("DeactiveMagic", deactiveTimer);
        
    }

    private void OnDisable()
    {
        rb.velocity = Vector2.zero;

        if (getTrailRanderer)
        {
            trail.Clear();
        }
    }

    //ŠÖ”(“®‚­•ûŒü‚ğİ’è‚·‚éŠÖ”A”ñ•\¦‚É‚·‚éŠÖ”)

    public void MoveDirection(Vector3 direction)
    {
        rb.velocity = direction * moveSpeed;
    }

    void DeactiveMagic()
    {
        if (destoryObj) 
        { 
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }


    //“–‚½‚è”»’è

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") ||
            collision.CompareTag("Boss"))
        {
            rb.velocity = Vector2.zero;

            CancelInvoke("DeactiveMagic");


            if (!damage)
            {
                damage= true;

                collision.GetComponent<Health>().TakeDamage(damageAmount);
            }



            DeactiveMagic();
        }


        if (collision.CompareTag("Blocking"))
        {
            rb.velocity = Vector2.zero;

            CancelInvoke("DeactiveMagic");

            DeactiveMagic();
        }

    }
}
