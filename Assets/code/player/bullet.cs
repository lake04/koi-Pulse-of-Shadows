using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class bullet : MonoBehaviour
{
    public EnemyInfo enemy;
    private Rigidbody2D rb;
    public player player;
    private float speed = 7f;
    public ObjectFollowMousePosition follow;
    Vector2 direction;

    private void Awake()
    {
       rb = GetComponent<Rigidbody2D>();
       player = FindAnyObjectByType<player>();
       follow = FindAnyObjectByType<ObjectFollowMousePosition>();
    }

    private void Start()
    {
        GameObject taget = GameObject.Find("taget");

        Transform tagetPos = follow.transform;
        direction = (tagetPos.position - transform.position).normalized;

        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Debug.Log("attack");
            collision.GetComponent<EnemyInfo>().onDamge();
            Destroy(this.gameObject);
        }
    }
}
