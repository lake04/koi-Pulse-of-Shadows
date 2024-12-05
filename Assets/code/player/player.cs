using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class player : MonoBehaviourPunCallbacks
{
    #region 플레이어 정보
    [Header("player")]
    public GameManger gameManger;
    public float moveSpeed = 5f;
    public float attackTimde = 2f;
    public bool isdamage = true;

    #endregion

    #region 대쉬
    [Header("dash")]
    public bool canDash = true;
    private bool isDashing;
    public float dashSpeed = 4f;
    private float dashCoolDown = 1f;
    private float effecting = 1f;
    #endregion

    #region effect
    [Header("effect")]
    public TrailRenderer trail;
    public GameObject hitUi;

    //카메라 흔들림
    private float shakeTime;
    private float shakeIntensity;
    private float camerTime = 0.2f;
    public ParticleSystem ps;
    #endregion

    public Rigidbody2D rigidbody;
    public RaycastHit hit;
    public Vector3 moveDistance = Vector3.zero;
    public PhotonView PV;
    public Material[] mat = new Material[2];
    SpriteRenderer sprite;

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
        Screen.SetResolution(1980, 1080, true);
        mainCamera = Camera.main;
        /*PhotonNetwork.ConnectUsingSettings();*/
        GameObject.FindWithTag("Hit");
        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        isdamage = true;
        gameManger = FindAnyObjectByType<GameManger>();
      
        Instantiate(ps);
        
        sprite.material = PV.IsMine ? mat[0] : mat[1];
    }

    void Update()
    {
        ps.transform.position = transform.position * 0.5f;
        hitUi.transform.position = new Vector3(0,0,0);

        if (gameManger.ismulti == false)
        {
            move();
        }
        if (gameManger.ismulti == true && PV.IsMine)
        {
            RPC_move();
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            OnShakeCamera(0.2f, 0.09f);
            StartCoroutine(desh());
            Debug.Log("대쉬");
        }

        if(Input.GetKeyDown (KeyCode.V))
            {
            gameManger.hp = 100000;
        }
    }

    #region 충돌 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isdamage == true)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                onDamage();
                Debug.Log("damage");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isdamage == true)
        {
            if (collision.gameObject.CompareTag("laser"))
            {
                onDamage();
                Debug.Log("damage");
            }
            else if (collision.gameObject.CompareTag("pt2"))
            {
                onDamage();
                Debug.Log("damage");
            }

            else if (collision.gameObject.CompareTag("enemy"))
            {
                onDamage();
                Debug.Log("damage");
            }
        }
    }
    #endregion

    public void onDamage()
    {
        if (gameManger.hp > 0)
        {
            gameManger.hp--;
            StartCoroutine(hitAnim());
            OnShakeCamera(0.3f);
            if (gameManger.hp <= 0 && PV.IsMine)
            {
                SceneManager.LoadScene(0);
                Destroy(this.gameObject);
                PhotonNetwork.Disconnect();
            }
        }
        else if (gameManger.hp <= 0 && PV.IsMine)
        {
            SceneManager.LoadScene(0);
            Destroy(this.gameObject);
            PhotonNetwork.Disconnect();
        }
    }

    private IEnumerator desh()
    {
        canDash = false;
        trail.emitting = true;
        StartCoroutine(coolTime());
        transform.position += moveDistance * dashSpeed;
     
        yield return new WaitForSeconds(dashCoolDown);
        trail.emitting = false;
        canDash = true;
        /*  yield return new WaitForSeconds(effecting);*/
    }


    public void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDistance = new Vector3(x, y, 0);
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
    }

    [PunRPC]
    public void RPC_move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDistance = new Vector3(x, y, 0);
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
    }

    #region camera
    IEnumerator coolTime()
    {
        isdamage = false;  // 무적 상태 시작
        yield return new WaitForSeconds(0.8f);  // 무적 시간 동안 대기
        isdamage = true;  // 무적 상태 해제
    }
    IEnumerator hitAnim()
    {
        hitUi.SetActive(true);
        StartCoroutine(coolTime());
        yield return new WaitForSeconds(0.2f);
        
        hitUi.SetActive(false);
    }

    //카메라
    public void OnShakeCamera(float shakeTime = 0.2f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;
        StopCoroutine(shakebyPosition());
        StartCoroutine(shakebyPosition());
    }

    //카메라
    private IEnumerator shakebyPosition()
    {
        Vector3 startPosition = mainCamera.transform.position;

        while (shakeTime > 0.0f)
        {
            mainCamera.transform.position = startPosition + UnityEngine.Random.insideUnitSphere * shakeIntensity;

            shakeTime -= Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.position = startPosition;
    }
    #endregion
}
