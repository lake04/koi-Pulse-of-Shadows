using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public GameObject multiUi;
    public spawn spawn;
    public GameObject logo;
    public GameObject lodaingText;

    private void Awake()
    {
        
        Screen.SetResolution(1980, 1080,true);
        StartCoroutine(nameof(lodaing));
        PhotonNetwork.ConnectUsingSettings();
        spawn.isSpawn = false;

        Application.runInBackground = true;

        PhotonNetwork.AutomaticallySyncScene = true;
    }

  
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버에 연결되었습니다.");
    }

    public void Create() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
 

    public  void Join() =>PhotonNetwork.JoinRoom("Room");


    public void play()
    {
        PhotonNetwork.Instantiate("player",Vector3.zero,Quaternion.identity);
        multiUi.SetActive(false);
        spawn.isSpawn = true;
    }

    private IEnumerator lodaing()
    {
        yield return new WaitForSeconds(3.0f);
        logo.SetActive(false);
        lodaingText.SetActive(false);
    }
}
