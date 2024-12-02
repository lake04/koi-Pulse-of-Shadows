using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;

public class Fire : MonoBehaviourPunCallbacks
{
    private Rigidbody2D rb;
    public player player;
    public float shootCoolTime = 0.5f;
    public bool isShoot = true;
    ObjectFollowMousePosition follow;
    Vector2 direction;
    public GameObject prefabBullet;
    public PhotonView PV;
    public GameManger gm;
    AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<player>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gm = FindAnyObjectByType<GameManger>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isShoot && gm.ismulti == false)
        {
            audioManager.PlaySFX(audioManager.bullet);
            Debug.Log("น฿ป็!!!!!!");
            StartCoroutine(shoot());
            player.OnShakeCamera(0.2f,0.09f);
        }

        if (Input.GetMouseButtonDown(0) && isShoot && gm.ismulti == true && PV.IsMine)
        {
            audioManager.PlaySFX(audioManager.bullet);
            player.OnShakeCamera(0.2f, 0.09f);
            PV.RPC("RPC_shoot", RpcTarget.AllBuffered);
        }
    }

    
    public IEnumerator shoot()
    {
        isShoot = false;
        GameObject bullet = Instantiate(prefabBullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootCoolTime);
        isShoot = true;
    }

    [PunRPC]
    protected IEnumerator RPC_shoot()
    {
        isShoot = false;
        GameObject bullet = Instantiate(prefabBullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootCoolTime);
        isShoot = true;
    }
}
