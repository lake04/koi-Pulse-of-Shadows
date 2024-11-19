using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card 
{
    public string cardName;
    public int weight;

    public Card(Card card)
    {
        this.cardName = card.cardName;
        this.weight = card.weight;
    }
}
