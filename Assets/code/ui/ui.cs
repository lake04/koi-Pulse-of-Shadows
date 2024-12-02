using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Photon.Pun;

public class ui : MonoBehaviour
{
    public FadeEffect fadeEffect;
    public GameObject pop;
    public GameObject modePop;
    public GameObject quitPop;
    public GameObject other;
    public spawn spawn;
    public GameManger gm;

    private void Start()
    {
        popDown();
        modePop.SetActive(false);
        quitPop.SetActive(false);
        DontDestroyOnLoad(pop);
    }
        
    public void modePopup()
    {
        modePop.SetActive(true);
        other.SetActive(false);
    }
    public void play()
    {
        SceneManager.LoadScene(1);
        PhotonNetwork.Disconnect();
        spawn.isSpawn = true;
    }
    public void multi()
    {
        gm.ismulti = true;
        SceneManager.LoadScene(2);
        gm.ismulti = true;
    }
    public void quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void popUP()
    {
        pop.SetActive(true);
    }

    public void popDown()
    {
        pop.SetActive(false);
    }

    public void QupopUP()
    {
        quitPop.SetActive(true);
    }

    public void QupopDown()
    {
        quitPop.SetActive(false);
    }
}
