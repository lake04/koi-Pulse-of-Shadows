using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class player : MonoBehaviour
{
    #region º¯¼öµé
    [Header("player")]
    public int hp =5;
    public float damge =2f;
    public float ex;
    public float gold;
    public float deshSpeed = 5f;
    public float moveSpeed = 7f;

    [Header("effect")]
    public TrailRenderer trail;

    public Vector3 moveDistance = Vector3.zero;
    public Rigidbody2D rigidbody2D;
    public RaycastHit hit;
    #endregion

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            desh();
        }
        else
        {
            trail.emitting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            onDamge();
            Debug.Log("dagme");
        }

    }
    public void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDistance = new Vector3(x, y, 0);
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
    }

     private void desh()
    {
        transform.position += moveDistance * deshSpeed;
        trail.emitting = true;
    }

    public void onDamge()
    {

        if(hp>0)
        {
            hp--;
        }
        
        else if( hp <=0 )
        {
            Destroy(this.gameObject);
        }
    }
}
