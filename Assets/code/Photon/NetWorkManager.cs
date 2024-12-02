using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviourPunCallbacks
{
    public GameObject multiUi;
    public spawn spawn;
    public GameObject logo;
    public GameObject lodaingText;
    public Text ListText;
    public Text RoomInfoText;
    public InputField NickNameInput;
    public InputField CreateInput;
    public PhotonView PV;
    public GameStopRewume GameStopRewume;
    public GameManger gm;

    private void Awake()
    {
        Screen.SetResolution(1980, 1080,true);
        StartCoroutine(nameof(lodaing));
        PhotonNetwork.ConnectUsingSettings();
        spawn.isSpawn = false;
        gm.ismulti = true;
        PV = GetComponent<PhotonView>();
        Application.runInBackground = true;

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("서버에 연결되었습니다.");
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
    }

    public void Create()
    {
        PhotonNetwork.JoinOrCreateRoom(CreateInput.text, new RoomOptions { MaxPlayers = 2 }, null);
        RoomRenewal();
    }
    public void Join()
    {
        PhotonNetwork.JoinRoom(CreateInput.text);
        gm.ismulti = true;
        RoomRenewal();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity);
        RoomRenewal();
    }

    [PunRPC]
    public void play()
    {
        if (PV.IsMine)
        {
            multiUi.SetActive(false);
            /*  spawn.isSpawn = true;*/
           
            photonView.RPC("RequestSceneChange", RpcTarget.AllBuffered);
        }
    }

    private IEnumerator lodaing()
    {
        spawn.isSpawn = false;
        yield return new WaitForSeconds(3.0f);
        logo.SetActive(false);
        lodaingText.SetActive(false);
    }
    void RoomRenewal()
    {
        ListText.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            ListText.text += PhotonNetwork.PlayerList[i].NickName + ((i + 1 == PhotonNetwork.PlayerList.Length) ? "" : "\n");
        RoomInfoText.text = PhotonNetwork.CurrentRoom.Name + " / " + PhotonNetwork.CurrentRoom.PlayerCount + "명 / " + PhotonNetwork.CurrentRoom.MaxPlayers + "최대";
    }

    [PunRPC]
    private void RequestSceneChange()
    {
        multiUi.SetActive(false);
        GameStopRewume.RPC_GameStart();
    }
}
