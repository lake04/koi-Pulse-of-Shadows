using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class pattern : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject spawnPos;
    private float pattenCoolTime = 2f;
    private float spawnCycle; //�� ���� �ֱ�
    private bool ispatten = true;
    float random_num;


    private void Awake()
    {
        //Collider2D collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        
       
    }

    void Start()
    {
        InvokeRepeating("spawn", 1f, 1.3f);
    }


    IEnumerator Pattern1()
    {
        Instantiate(enemy,transform.position,quaternion.identity);
        ispatten = true;
        yield return new WaitForSeconds(pattenCoolTime);
    }

    private void spawn()
    {
        random_num = UnityEngine.Random.Range(2, 5);
        Debug.Log(random_num);
        if (random_num == 2)
        {
            StartCoroutine(Pattern1());
        }
        if (random_num == 3)
        {
            Debug.Log("2");
        }
        if (random_num == 4)
        {
            Debug.Log("3");
        }
    }

}
