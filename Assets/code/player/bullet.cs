using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
    public enemy enemy;
    private Rigidbody2D rb;
    public player player;
    private float speed = 7f;
  

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       player = GetComponent<player>();
    }
    private void Update()
    {
      transform.Translate(Vector2.right * speed * Time.deltaTime);
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