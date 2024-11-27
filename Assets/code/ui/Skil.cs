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

                        Init(data);
                        HpUP();

                    break;
                case SkilData.SkilType.damage_up:
                    if (level < data.damages.Length)
                    {
                        Init(data);
                        DamageUP();
                    }
                    break;
            }

            level++;
            gameManger.skillPoint = 0;
            gameManger.exMax = gameManger.exMax + 5;
            gameStopRewume.GameResume();
        }
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
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
            Debug.Log("Healing");
            gameManger.hp = gameManger.hp + 5;
            level++;
            gameManger.skillPoint = 0;
            gameManger.exMax = gameManger.exMax + 5;
        }
    }
}
