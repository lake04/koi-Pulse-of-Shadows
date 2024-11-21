using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    #region 플레이어 정보
    [Header("player")]
    public int hp = 5;
    public int maxhp = 5;
    public int dmage = 2;
    public float moveSpeed = 7f;
    public float attackTimde = 2f;
    #endregion

    #region 대쉬
    [Header("dash")]
    public bool canDash =true;
    private bool isDashing;
    public float dashSpeed = 5f;
    private float dashingTime = 0.5f;
    private float dashCoolDown = 0.5f;
    private float effecting =1f;
    #endregion

    #region effect
    [Header("effect")]
    public TrailRenderer trail;
    #endregion 

    public Rigidbody2D rigidbody;
    public RaycastHit hit;
    public Vector3 moveDistance = Vector3.zero;

    #region bullet
    [Header("bullet")]
    private Camera mainCamera;
   
    public UnityEngine.Transform bulletPos;
    public float bulletSpeed = 20f;
    public float shootCoolTime = 0.5f;
    public bool isShoot = true;
    #endregion

    private void Awake()
    {
        mainCamera = Camera.main;
        rigidbody  = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        move();

         if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine (desh());
        }
    }

    #region 충돌 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            onDamge();
            Debug.Log("damage");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("laser"))
        {
            onDamge();
            Debug.Log("damage");
        }
        else if (collision.gameObject.CompareTag("pt2"))
        {
            onDamge();
            Debug.Log("damage");
        }
    }
    #endregion 

    public void onDamge()
    {
        if (hp > 0) hp--;

        else if (hp <= 0)
        {
            SceneManager.LoadScene(1);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator desh()
    {
        canDash = false;
        isDashing = true;

        transform.position += moveDistance * dashSpeed;

        trail.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        yield return new WaitForSeconds(effecting);
        trail.emitting = false;
    }

    public void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDistance = new Vector3(x, y, 0);
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
    }
}
