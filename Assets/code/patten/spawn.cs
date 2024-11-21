using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer
{

}

public class spawn : MonoBehaviour
{
    public GameObject rangeObject;
    BoxCollider2D rangeCollider;
    public GameObject capsul;
    [SerializeField]
    private GameObject pt2;
    [SerializeField]
    private GameObject l;
    public int enemyCount;
    public int totalEnemy =0;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
       /* StartCoroutine(RandomRespawn_Coroutine());*/
        StartCoroutine(laserPatten());
        StartCoroutine(patten2());
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position;
        // �ݶ��̴��� ����� �������� bound.size ���
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector3 RandomPostion = new Vector3(range_X, range_Y, 0f);

        Vector3 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
    private void Update()
    {
        if (enemyCount < totalEnemy)
        {
            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(capsul, Return_RandomPosition(), Quaternion.identity);
            enemyCount++;
        }
    }

    IEnumerator patten2()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(pt2, Return_RandomPosition(), Quaternion.identity);
        }
    }

    IEnumerator laserPatten()
    {
        while (true)
        {
            List<lazer> lassers = new List<lazer>();
            for (int i = 0; i < 15; i++)
            {
                GameObject g = Instantiate(l, Return_RandomPosition(), Random.rotation);
                yield return new WaitForSeconds(0.3f);
                Destroy(g);
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(5, 10));
        }
    }
}
