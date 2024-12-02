using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class GameStopRewume : MonoBehaviourPunCallbacks
{
    public GameObject CountText = null;
    Text Text = null;
    public spawn spawn;
    public GameManger gm;
    public PhotonView PV;

    public float GameTime = 1.5f;
    int CountDown = 3;

    // Start is called before the first frame update
    void Start()
    {
        Text = CountText.GetComponent<Text>();
        CountText.SetActive(false);

        PV = GetComponent<PhotonView>();
        if(gm.ismulti == true && PV.IsMine)
        {
            spawn.isSpawn = false;
        }

        else if(gm.ismulti == false)
        {
            GameStart();
        }
    }

    public void GameStop()
    {
        GameTime = Time.timeScale;
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        CountDown = 3;

        CountDownFun();
    }

    public void GameStart()
    {
        Time.timeScale = 0;
        CountDown = 3;
        CountDownFun();
    }

    [PunRPC]
    public void RPC_GameStart()
    {
        Time.timeScale = 0;
        CountDown = 3;
        CountDownFun();
    }

    public void CountDownFun()
    {
        CountText.SetActive(true);
        Text.text = CountDown.ToString();

        StartCoroutine(CountDownStart());
    }

    IEnumerator CountDownStart()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            CountDown -= 1;

            if (CountDown == 0)
            {
                Time.timeScale =1;
                CountText.SetActive(false);
                spawn.isSpawn = true;
                yield return null;
            }

            Text.text = CountDown.ToString();
        }
    }

   
}

