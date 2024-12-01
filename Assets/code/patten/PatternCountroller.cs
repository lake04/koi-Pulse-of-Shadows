using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCountroller : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private GameObject[] pattenrs;  //�����ϰ� �ִ� ��� ����
    private GameObject currentPattern; //���� ������� ����
    private int[]     patternIndexs;    //��ġ�� �ʴ� patterns.Length ������ ����
    private int current = 0;            //patternIndexs �迭�� ����
    public spawn spawn;

    private void Awake()
    {
        //�����ϰ� �ִ� ���� ������ �����ϰ� �޸� �Ҵ�
        patternIndexs = new int[pattenrs.Length];

        //ó������ ������ ���������� �����ϵ��� ����
        for(int i = 0; i < pattenrs.Length; ++i)
        {
            patternIndexs[i] = i;
        }
    }

    private void Update()
    {
        if (spawn.isSpawn == true)
        {
            ChangePattern();
        }
      
        //���� ������� ������ ����Ǿ� ������Ʈ�� ��Ȱ��ȭ�Ǹ�
        /* if(currentPattern.activeSelf == false)
          {
              //���� ����
              ChangePattern();
          }*/
    }

    public void GameOver()
    {
        currentPattern.SetActive(false);
    }
    public void ChangePattern()
    {
        //���� ���� ����
        currentPattern = pattenrs[patternIndexs[current]];

        currentPattern.SetActive (true);

        current++;

        //������ �ѹ��� ��� �����ߴٸ� ���� ������ ��ġ�� �ʴ� ������ ���ڷ� ����
        if(current>=patternIndexs.Length)
        {
            patternIndexs = Utils.RandomNubers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }
}
