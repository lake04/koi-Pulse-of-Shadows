using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Rigidbody2D rb;
    public player player;
    public float shootCoolTime = 0.5f;
    public bool isShoot = true;
    ObjectFollowMousePosition follow;
    Vector2 direction;
    public GameObject prefabBullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindAnyObjectByType<player>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isShoot)
        {
            StartCoroutine(shoot());
            player.OnShakeCamera(0.2f,0.05f);
        }
    }

    public IEnumerator shoot()
    {
        isShoot = false;
        GameObject bullet = Instantiate(prefabBullet,transform.position, transform.rotation);
        yield return new WaitForSeconds(shootCoolTime);
        isShoot = true;
    }
}
