using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks {

    public static PhotonLobby lobby;

    public GameObject battleButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    // Use this for initialization
    void Start () {
        PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player connected to master server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        battleButton.SetActive(true);
    }

    public void OnBattleButtonClicked()
    {
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join random room failed.");
        CreateRoom();
    }

    void CreateRoom()
    {
        int random = Random.Range(0, 1000);
        RoomOptions opt = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers };
        PhotonNetwork.CreateRoom("Room" + random, opt);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed. Trying again...");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        battleButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
