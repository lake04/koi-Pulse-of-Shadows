using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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

    AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<player>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isShoot && PV.IsMine)
        {
            audioManager.PlaySFX(audioManager.bullet);
            StartCoroutine(shoot());
            player.OnShakeCamera(0.2f,0.09f);
        }
    }

    [PunRPC]
    public IEnumerator shoot()
    {
        isShoot = false;
        GameObject bullet =Instantiate(prefabBullet,transform.position, transform.rotation);
        yield return new WaitForSeconds(shootCoolTime);
        isShoot = true;
    }
}
