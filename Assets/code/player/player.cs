using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    #region �÷��̾� ����
    [Header("player")]
    public GameManger gameManger;
    public float moveSpeed = 7f;
    public float attackTimde = 2f;
    public bool isdamage;
    #endregion

    #region �뽬
    [Header("dash")]
    public bool canDash =true;
    private bool isDashing;
    public float dashSpeed = 4f;
    private float dashCoolDown = 1f;
    private float effecting =1f;
    #endregion

    #region effect
    [Header("effect")]
    public TrailRenderer trail;
    public GameObject hitUi;

    //ī�޶� ��鸲
    private float shakeTime;
    private float shakeIntensity;
    private float camerTime = 0.2f;
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
        hitUi.SetActive(false);
    }

    void Update()
    {
        move();

         if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine (desh());
        }
    }

    #region �浹 ó��
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
    }
    #endregion

    public void onDamage()
    {
        if (gameManger.hp > 0)
        {
            gameManger.hp --;
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

    #region camera
    IEnumerator coolTime()
    {
        isdamage = true;  // ���� ���� ����
       
        yield return new WaitForSeconds(1.2f);  // ���� �ð� ���� ���
        isdamage = false;  // ���� ���� ����
    }
    IEnumerator hitAnim()
    {
        hitUi.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitUi.SetActive(false);
    }

    //ī�޶�
    public void OnShakeCamera(float shakeTime = 0.2f, float shakeIntensity = 0.1f)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensity = shakeIntensity;
        StopCoroutine(shakebyPosition());
        StartCoroutine(shakebyPosition());
    }

    //ī�޶�
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
