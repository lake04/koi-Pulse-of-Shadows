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

public class player : MonoBehaviour
{
    #region 플레이어 정보
    [Header("player")]
    public GameManger gameManger;
    public float moveSpeed = 7f;
    public float attackTimde = 2f;
    public bool isdamage;
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
        GameObject.FindWithTag("Hit");
        rigidbody = GetComponent<Rigidbody2D>();
        hitUi.SetActive(false);
        gameManger = FindAnyObjectByType<GameManger>();
        Instantiate(ps);
        Instantiate(hitUi);

    }

    void Update()
    {
        ps.transform.position = transform.position * 0.5f;

        if (PV.IsMine)
        {
            move();
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            OnShakeCamera(0.2f, 0.09f);
            StartCoroutine(desh());
        }
    }

    #region 충돌 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            onDamage();
            Debug.Log("damage");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
    #endregion

    public void onDamage()
    {
        if (gameManger.hp > 0)
        {
            gameManger.hp--;
            StartCoroutine(hitAnim());
            StartCoroutine(coolTime());
            OnShakeCamera(0.3f);
            if (gameManger.hp <= 0)
            {
                SceneManager.LoadScene(0);
                Destroy(this.gameObject);
            }
        }
        else if (gameManger.hp <= 0)
        {
            SceneManager.LoadScene(0);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator desh()
    {
        canDash = false;
        trail.emitting = true;
        transform.position += moveDistance * dashSpeed;
        dashEffect();
        yield return new WaitForSeconds(dashCoolDown);
        trail.emitting = false;
        canDash = true;

        /*  yield return new WaitForSeconds(effecting);*/

    }

    IEnumerator dashEffect()
    {
        

        yield return new WaitForSeconds(dashCoolDown);
    }
    public void move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        moveDistance = new Vector3(x, y, 0);
        transform.position += moveDistance * moveSpeed * Time.deltaTime;
    }

    #region camera
    IEnumerator coolTime()
    {
        isdamage = true;  // 무적 상태 시작
       
        yield return new WaitForSeconds(1.2f);  // 무적 시간 동안 대기
        isdamage = false;  // 무적 상태 해제
    }
    IEnumerator hitAnim()
    {
        hitUi.SetActive(true);
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
