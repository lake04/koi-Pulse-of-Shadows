using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pt4 : MonoBehaviour
{
  

    [SerializeField]
    private GameObject[] warningImages;
    [SerializeField]
    private GameObject[] enemyObjects;
    [SerializeField]
    private float spawnCycle = 1;

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        //다음 사용을 위해 플레이어 오브젝트의 상태를 초기화
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyObjects[i].SetActive(false);
            enemyObjects[i].GetComponent<EnenmyRoating>();
        }
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        //플레이어의 등장 위치를 겹치지 않는 임의의 위치로 설정
        int[] numbers = Utils.RandomNubers(3, 3);

        int index = 0;
        while (index < numbers.Length)
        {
            StartCoroutine(nameof(SpawnEnemy), index);

            index++;

            yield return new WaitForSeconds(spawnCycle);
        }

        //패턴 종료 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
    private IEnumerator SpawnEnemy(int index)
    {
        //경고 이미지 활설/비활성
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        //플레이어의 오브젝트 활성화
        enemyObjects[index].SetActive(true);
    }
}
