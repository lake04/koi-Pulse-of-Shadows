using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Card 
{
    public string cardName;
    public int weight;
    public int upHp;
    public int dmagaeUP;
    public int healing;

   
    public Card(Card card)
    {
        this.cardName = card.cardName;
        this.weight = card.weight;
        this.upHp = card.upHp;
        this.dmagaeUP = card.dmagaeUP;
        this.healing = card.healing;
    }
}
