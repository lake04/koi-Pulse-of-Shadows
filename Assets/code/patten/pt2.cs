using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pt2 : MonoBehaviour
{
    [SerializeField]
    private GameObject pt;
    [SerializeField]
    private spawn spawn;

    private void Start()
    {
        StartCoroutine(patten2());
    }


    IEnumerator patten2()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            // ���� ��ġ �κп� ������ ���� �Լ� Return_RandomPosition() �Լ� ����
            GameObject instantCapsul = Instantiate(pt, spawn.Return_RandomPosition(), Quaternion.identity);
        }
    }
}
