using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class player : MonoBehaviour
{
    #region 플레이어 정보
    [Header("player")]
    public int hp = 5;
    public int dmage = 2;
    public float ex;
    public float gold;
   
    public float moveSpeed = 7f;
    public float attackTimde = 2f;
    #endregion
    [Header("dash")]
    private bool canDash =true;
    private bool isDashing;
    public float dashSpeed = 5f;
    private float dashingTime = 0.2f;
    private float dashCoolDown = 0.2f;
    private float effecting =1f;

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
    private Vector3 rayDistance;

    #region bullet
    [Header("bullet")]
    public GameObject prefabBullet;
    public GameObject bulletPos;
    public float bulletSpeed = 20f;
    public float shootCoolTime = 0.5f;
    public bool isShoot = true;
    Vector3 dir; //마우스 포인터 방향을 저장할 변수
    Camera cam; 
    #endregion

    private void Awake()
    {
        cam = Camera.main;
        rigidbody2D = GetComponent<Rigidbody2D>();
        dashEffect.SetActive(false);
        ain = GetComponent<Animator>();

        rayDistance = new Vector3(transform.position.x+1,transform.position.y+1,0);
    }

    void Update()
    {
        move();

      


        if (Input.GetMouseButtonDown(0) && isShoot)
        {
            dir = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
            StartCoroutine(shoot());
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            if (Physics.Raycast(transform.position,transform.forward ,out hit))
            {
                
                if (hit.transform.CompareTag("wall"))
                {
                    Debug.Log("wall");
                }
            }
            StartCoroutine (desh());
        }
       
        else
        {
            trail.emitting = false;
        }

        Debug.DrawRay(transform.position, rayDistance, new Color(0, 20, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, moveDistance, 20, LayerMask.GetMask("wall"));
        if (rayHit.collider != null)
        {
            Debug.Log("wall");
            dashSpeed = 2;
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
        rayDistance = transform.position + moveDistance;

        transform.position += moveDistance * dashSpeed;
        

        trail.emitting = true;
        yield return new WaitForSeconds(dashingTime);

        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        yield return new WaitForSeconds(effecting);
      
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
        GameObject bullet = Instantiate(prefabBullet,bulletPos.transform.position,Quaternion.identity);
        bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed, ForceMode2D.Impulse);

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
