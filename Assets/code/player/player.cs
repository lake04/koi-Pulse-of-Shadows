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

public class player : MonoBehaviour
{
    #region 플레이어 정보
    [Header("player")]
    public int hp = 5;
    public int dmage = 2;
   
    public float gold;
   
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
    public GameObject dashEffect;
    public GameObject dashEffextPos;
    public Animator ain;
    public bool isShake;
    #endregion 

    public Rigidbody2D rigidbody2D;
    public RaycastHit hit;

    public Vector3 moveDistance = Vector3.zero;

    #region bullet
    [Header("bullet")]
    private Camera mainCamera;

    public GameObject prefabBullet;
    public UnityEngine.Transform bulletPos;
    public float bulletSpeed = 20f;
    public float shootCoolTime = 0.5f;
    public bool isShoot = true;
    Vector3 dir; //마우스 포인터 방향을 저장할 변수
    #endregion

    private void Awake()
    {
        mainCamera = Camera.main;
        rigidbody2D = GetComponent<Rigidbody2D>();
        dashEffect.SetActive(false);
        ain = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 len = mainCamera.ScreenToWorldPoint(Input.mousePosition)- transform.position;
        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        transform.rotation = quaternion.Euler(0, 0, z);
        move();

        if (Input.GetMouseButtonDown(0) && isShoot)
        {
            /*dir = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));*/
            StartCoroutine(shoot());
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine (desh());
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
    private IEnumerator desh()
    {
        canDash = false;
        isDashing = true;
      
        isShake = true;

        transform.position += moveDistance * dashSpeed;
        

        trail.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        yield return new WaitForSeconds(effecting);
        trail.emitting = false;
        dashEffect.SetActive(false);

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

    public IEnumerator shoot() 
    {
        isShoot = false;
        GameObject bullet = Instantiate(prefabBullet, bulletPos.transform.position, transform.rotation);
        /*bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);*/

        yield return new WaitForSeconds(shootCoolTime);
        isShoot = true;
    }

    public void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDistance = new Vector3(x, y, 0);
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
    }
}
