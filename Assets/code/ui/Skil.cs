using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skil : MonoBehaviour
{
    public SkilData data;
    public int level;

    public SkilData.SkilType type;
    public int rate;
    public GameManger gameManger;
    public GameStopRewume gameStopRewume;
    public spawn spawn;
    public EnemyInfo enemyInfo;

    Image icon;
    Text textLevel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.skilIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
    }

    private void LateUpdate()
    {
        textLevel.text = "LV." + (level+1);
    
    }

    public void Onclick()
    {
        if (gameManger.skillPoint > 0)
        {
            switch (data.skilType)
            {
                case SkilData.SkilType.hp_up:

                    gameManger.maxhp = + 2;
                    gameManger.hp = gameManger.maxhp;
                    level++;
                    gameManger.exMax = gameManger.exMax + 5;
                    break;
                case SkilData.SkilType.damage_up:
                        gameManger.damage = gameManger.damage + 2;
                    level++;
                    gameManger.exMax = gameManger.exMax + 5;
                    break;
            }
        }
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
        gameManger.skillPoint = 0;
        enemyInfo.hp += 1;
        gameStopRewume.GameResume();
    }

    public void Init(SkilData data)
    {
        //Property Set
        type = data.skilType;
        rate = data.damages[level];
    }


    void DamageUP()
    {
        Debug.Log("damageUP");
       
        gameManger.damage = gameManger.damage + rate; 
    }

    void HpUP()
    {
        Debug.Log("HpUP");
        gameManger.maxhp =gameManger.maxhp + rate;
        gameManger.hp = gameManger.maxhp;
    }

    public void Healing()
    {
        if (gameManger.skillPoint > 0)
        {
            
            gameManger.hp = gameManger.hp + 5;
            if (gameManger.hp >= gameManger.maxhp)
            {
                gameManger.hp = gameManger.maxhp;
            }
            level++;
            enemyInfo.hp += 4;
            gameManger.skillPoint = 0;
            gameManger.exMax = gameManger.exMax + 5;
        }
        gameStopRewume.GameResume();
    }
}
