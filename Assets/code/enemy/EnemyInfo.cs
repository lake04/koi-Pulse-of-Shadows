using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyInfo : MonoBehaviour
{
    #region enemy¡§∫∏
    [Header("enemy")]
    public int hp = 2;
    public float distance;
    public LayerMask isLayer;
    public float speed;
    private int rotateSpeed;

    public player player;
    Transform playerTransform;
    public  Rigidbody2D rigidbody2D;
    public spawn sp;
    public GameObject dieEffect;
    #endregion

    public GameManger manger;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        manger = FindAnyObjectByType<GameManger>();
        player = FindAnyObjectByType<player>();
        sp = FindAnyObjectByType<spawn>();
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        Vector2 direction = new Vector2(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y);

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction,isLayer);
        Debug.DrawRay(transform.position, direction, new Color(0, 1, 0));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotateSpeed * Time.deltaTime);

        transform.rotation = rotation;

        Vector3 dir = direction;
        transform.position += (-dir.normalized * speed * Time.deltaTime);
    }

    public void onDamage()
    {
        if (hp >0)
        {
            hp -=manger.damage;
           if (hp <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                manger.ex++;
                sp.enemyCount--;
            }
        }
        else if (hp <=0)
        {
            Destroy(this.gameObject);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            manger.ex++;
            sp.enemyCount--;
        }
    }
}
