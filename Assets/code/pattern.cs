using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class pattern : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private float pattenCoolTime = 2f;
    private bool ispatten = true;
    float random_num;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 1f, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
 
    IEnumerator Pattern1()
    {
        Instantiate(enemy);
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
