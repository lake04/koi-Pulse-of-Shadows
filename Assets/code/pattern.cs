using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class pattern : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject spawnPos;
    private float pattenCoolTime = 2f;
    private float spawnCycle; //적 생성 주기
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
        Instantiate(enemy,spawnPos.transform.position,quaternion.identity);
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

/*    private IEnumerator SpawnEnemys()
    {
        //패턴 시작 전 잠시 대기하는 시간
        float waitTime = 1;
        yield return new WaitForSeconds(waitTime);

        while (true)
        {
            Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
            Instantiate(enemy, position, Quaternion.identity);
            yield return new WaitForSeconds(spawnCycle);
        }
    }*/

}
