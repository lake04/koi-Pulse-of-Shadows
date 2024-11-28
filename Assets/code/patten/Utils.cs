using Unity.VisualScripting;
using UnityEngine;

public static class Utils
{
   /// <summary>
   /// 0~maxCouny������ ���� �� ��ġ�� �ʴ� n���� ������ �ʿ��� �� ���
   /// </summary>
   /// <param name="maxCount"></param>
   /// <param name="n"></param>
   /// <returns></returns>
   public static int[] RandomNubers(int maxCount,int n)
    {
        int[] defaults = new int[maxCount]; //0~maxCouny���� ������� �����ϴ� �迭
        int[] results = new int[n];

        //�迭 ��ü�� 0���� maxCount�� ���� ������� ����
        for (int i = 0; i<maxCount;++i)
        {
            defaults[i] = i;
        }

        //�츮�� �ʿ��� n���� ���� ����
        for (int i = 0; i < n; ++i)
        {
            int index = Random.Range(0, defaults.Length);

            results[index] = defaults[index];
            defaults[index] = defaults[maxCount-1];

            maxCount--;
        }
        return results;
    }
}
