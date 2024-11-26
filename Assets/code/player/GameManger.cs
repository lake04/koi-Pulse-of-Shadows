using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    internal static readonly object instance;
    public Text exeText;
    public int ex;
    public int exMax;
    public int[] exLevel;

    public int hp;
    public int maxhp;
    public int damage;

    public Text   hpText;
    public player player;

    private void Start()
    {
        player = FindAnyObjectByType<player>();
        hp = maxhp;
    }

    void Update()
    {
        exeText.text = "EX:"+ ex.ToString() + "/" + exMax.ToString();
        hpText.text  = "hp:" + hp.ToString()+ "/" + maxhp.ToString();

        
   }

    
}
