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
        //���� ����� ���� �÷��̾� ������Ʈ�� ���¸� �ʱ�ȭ
        for (int i = 0; i < enemyObjects.Length; i++)
        {
            enemyObjects[i].SetActive(false);
            enemyObjects[i].GetComponent<EnenmyRoating>();
        }
        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        //���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        //�÷��̾��� ���� ��ġ�� ��ġ�� �ʴ� ������ ��ġ�� ����
        int[] numbers = Utils.RandomNubers(3, 3);

        int index = 0;
        while (index < numbers.Length)
        {
            StartCoroutine(nameof(SpawnEnemy), index);

            index++;

            yield return new WaitForSeconds(spawnCycle);
        }

        //���� ���� �� ��� ����ϴ� �ð�
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
    private IEnumerator SpawnEnemy(int index)
    {
        //��� �̹��� Ȱ��/��Ȱ��
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        //�÷��̾��� ������Ʈ Ȱ��ȭ
        enemyObjects[index].SetActive(true);
    }
}
