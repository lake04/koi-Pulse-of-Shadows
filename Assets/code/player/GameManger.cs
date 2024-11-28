using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private TextMeshProUGUI textScore;

    private void Start()
    {
        player = FindAnyObjectByType<player>();
        hp = maxhp;
        ui.SetActive(false);
    }

    void Update()
    {
        exeText.text = "EX:" + ex.ToString() + "/" + exMax.ToString();
        hpText.text =  "Hp:" + hp.ToString() + "/" + maxhp.ToString();
        textScore.text = gameController.CurremtScore.ToString("F0");
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
          
        }
        else if (skillPoint <= 0)
        {
            ui.SetActive(false);
        }
    }

}
