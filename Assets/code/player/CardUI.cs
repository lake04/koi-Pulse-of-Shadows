using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour
{
 
    public Text cardName;

    // 카드의 정보를 초기화
    public void CardUISet(Card card)
    {
        cardName.text = card.cardName;
    }
}
