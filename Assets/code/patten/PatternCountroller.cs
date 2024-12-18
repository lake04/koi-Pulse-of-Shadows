using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCountroller : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject[] pattenrs;  //보유하고 있는 모든 패턴
    private GameObject currentPattern; //현재 재생중인 패턴
    private int[]     patternIndexs;    //겹치지 않는 patterns.Length 개수의 숫자
    private int current = 0;            //patternIndexs 배열의 순번
    public spawn spawn;
    public PhotonView PV;
    public GameManger gm;

    private void Awake()
    {
        //보유하고 있는 패턴 개수와 동일하게 메모리 할당
        patternIndexs = new int[pattenrs.Length];

        PV = GetComponent<PhotonView>();

        //처음에는 패턴을 순차적으로 실행하도록 설정
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
        //현제 패턴 변경
        currentPattern = pattenrs[patternIndexs[current]];

        currentPattern.SetActive (true);

        current++;

        //패턴을 한바퀴 모두 실행했다면 패턴 순서를 겹치지 않는 임의의 숫자로 설정
        if(current>=patternIndexs.Length)
        {
            patternIndexs = Utils.RandomNubers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }

    [PunRPC]
    public void RPC_ChangePattern()
    {
        //현제 패턴 변경
        currentPattern = pattenrs[patternIndexs[current]];

        currentPattern.SetActive(true);

        current++;

        //패턴을 한바퀴 모두 실행했다면 패턴 순서를 겹치지 않는 임의의 숫자로 설정
        if (current >= patternIndexs.Length)
        {
            patternIndexs = Utils.RandomNubers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }
}
