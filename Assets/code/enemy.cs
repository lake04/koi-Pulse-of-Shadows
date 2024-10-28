using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{
    [Header("enemy")]
    public float distance;
    public LayerMask isLayer;
    public float speed;
    private int rotateSpeed;

    public player player;
    Transform playerTransform;
    public  Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
   

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector2 direction = new Vector2(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y);

      

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);

        transform.rotation = rotation;

        Vector3 dir = direction;
        transform.position += (-dir.normalized * speed * Time.deltaTime);
    }

  
       
    
}
