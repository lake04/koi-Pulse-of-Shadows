using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCountroller : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject[] pattenrs;  //�����ϰ� �ִ� ��� ����
    private GameObject currentPattern; //���� ������� ����
    private int[]     patternIndexs;    //��ġ�� �ʴ� patterns.Length ������ ����
    private int current = 0;            //patternIndexs �迭�� ����
    public spawn spawn;
    public PhotonView PV;
    public GameManger gm;

    private void Awake()
    {
        //�����ϰ� �ִ� ���� ������ �����ϰ� �޸� �Ҵ�
        patternIndexs = new int[pattenrs.Length];

        PV = GetComponent<PhotonView>();

        //ó������ ������ ���������� �����ϵ��� ����
        for (int i = 0; i < pattenrs.Length; ++i)
        {
            patternIndexs[i] = i;
        }

        if (gm.ismulti == true && PV.IsMine)
        {
            for (int i = 0; i < pattenrs.Length; ++i)
            {
                patternIndexs[i] = i;
            }
        }
    }

    private void Update()
    {
        if (spawn.isSpawn == true && gm.ismulti == false)
        {
            ChangePattern();

        }

        if (spawn.isSpawn == true && gm.ismulti == true)
        {
            /* ChangePattern();*/
            PV.RPC("RPC_ChangePattern", RpcTarget.AllBuffered);
        }
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

    [PunRPC]
    public void RPC_ChangePattern()
    {
        //���� ���� ����
        currentPattern = pattenrs[patternIndexs[current]];

        currentPattern.SetActive(true);

        current++;

        //������ �ѹ��� ��� �����ߴٸ� ���� ������ ��ġ�� �ʴ� ������ ���ڷ� ����
        if (current >= patternIndexs.Length)
        {
            patternIndexs = Utils.RandomNubers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }
}
