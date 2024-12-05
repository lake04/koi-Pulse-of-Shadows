using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyInfo : MonoBehaviourPunCallbacks
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
    public  PhotonView PV;
    public GameManger gm;
    SpriteRenderer sprite;
    public Material[] mat = new Material[2];
    #endregion


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = FindAnyObjectByType<player>();
        sp = FindAnyObjectByType<spawn>();
        PV = GetComponent<PhotonView>();
        gm = FindAnyObjectByType<GameManger>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if (sp.isSpawn == false)
        {
            hp = 2;
        }
        player = FindAnyObjectByType<player>();
        gm = FindAnyObjectByType<GameManger>();
      
            Rotate();

       /* if (gm.ismulti == true && PV.IsMine)
        {
            PRC_Rotate();
        }*/
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
        transform.position += (-dir.normalized * speed * Time.deltaTime) ;
    }

    [PunRPC]
    private void PRC_Rotate()
    {
        Vector2 direction = new Vector2(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y);

        RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction, isLayer);
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
            hp -= gm.damage;
            StartCoroutine(DamgeEffecting());
            sprite.color = Color.red;
            if (hp <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                gm.ex++;
                sp.enemyCount--;
            }
        }
        else if (hp <=0)
        {
            Destroy(this.gameObject);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            gm.ex++;
            sp.enemyCount--;
        }
    }

    private IEnumerator DamgeEffecting()
    {
        sprite.material = mat[1];
        yield return new WaitForSeconds(0.2f);
        sprite.material = mat[0];
    }
}
