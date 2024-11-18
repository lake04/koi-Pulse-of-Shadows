using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AllyController : MonoBehaviour
{
    public LayerMask enemyLayer; //���̾� ����
    float FindRange = 2.5f; //����


    public player player;

    private void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var EnemyObj = Physics2D.OverlapCircle(transform.position, FindRange, enemyLayer);

        Debug.Log(EnemyObj);
        if(EnemyObj != null)
        {
            Debug.Log("nodash");
            player.canDash = false;
        }
        else
        {
            Debug.Log("dash");
            player.canDash = true;
        }
    }

    void OnDrawGizmos() // ���� �׸���
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FindRange);
    }
}
