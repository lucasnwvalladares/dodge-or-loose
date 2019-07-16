using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks {

    [SerializeField]
    private GameObject delayStartButton;
    [SerializeField]
    private GameObject delayCancelButton;
    [SerializeField]
    private int roomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        delayStartButton.SetActive(true);
    }

	// Use this for initialization
	public void DelayStart () {
        delayStartButton.SetActive(false);
        delayCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Tryin to Join Random Room From Delay Start!");
	}

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Joining at a random room failed. Let´s create one.");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating a room...");
        int randomRoomNumber = Random.Range(0, 100);
        RoomOptions roomOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MultiplayerSettings.multiplayerSettings.maxPlayers };
        string roomName = "Room" + randomRoomNumber;
        PhotonNetwork.CreateRoom(roomName, roomOpts);
        Debug.Log("Trying to create room: " + roomName + ".");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room. Trying again...");
        CreateRoom();
    }

    // Update is called once per frame
    public void DelayCancel () {
        delayCancelButton.SetActive(false);
        delayStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
	}
}
