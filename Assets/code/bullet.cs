using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public enemy enemy;
    private Rigidbody2D rb;

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("attack");
            collision.GetComponent<enemy>().onDamge();
            Die();
        }

        else if (collision.gameObject.CompareTag("wall"))
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

}
