using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameController : MonoBehaviour
{

    [SerializeField]
    private GameStopRewume stopRewume;


    private void Awake()
    {
        stopRewume.GameStart();
    }

}
