using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public enemy enemy;
    private Rigidbody2D rb;
    public player player;

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       player = GetComponent<player>();
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
