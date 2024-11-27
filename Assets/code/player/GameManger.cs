using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    internal static readonly object instance;
    public Text exeText;
    public int ex;
    public int exMax;
    public int skillPoint;

    public int hp;
    public int maxhp;
    public int damage;

    public Text hpText;
    public player player;
    public GameObject ui;
    public GameStopRewume GameStopRewume;

    private void Start()
    {
        player = FindAnyObjectByType<player>();
        hp = maxhp;
        ui.SetActive(false);
        GameStopRewume.GameStart();
       
    }

    void Update()
    {
        exeText.text = "EX:" + ex.ToString() + "/" + exMax.ToString();
        hpText.text = "hp:" + hp.ToString() + "/" + maxhp.ToString();

        if (ex >= exMax)
        {
            skillPoint = 1;
            GameStopRewume.GameStop();
            if (skillPoint > 0)
            {
                ui.SetActive(true);
            }
        }

        else if (skillPoint > 0)
        {
            ui.SetActive(true);
            GameStopRewume.GameStop();
        }
        else if (skillPoint <= 0)
        {
            ui.SetActive(false);
        /*   Time.timeScale = 1;*/
        }


    }

}
