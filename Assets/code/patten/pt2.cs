using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pt2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
