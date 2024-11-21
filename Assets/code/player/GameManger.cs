using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public Text exeText;
    public int ex;
    public int exMax = 5;

    public Text hpText;
    public player player;

    private void Start()
    {
        player = FindAnyObjectByType<player>();
    }

    void Update()
    {
        exeText.text = "EX:"+ ex.ToString() + "/" + exMax.ToString();
        hpText.text  = "hp:" + player.hp.ToString()+ "/" + player.maxhp.ToString();
    }
}
