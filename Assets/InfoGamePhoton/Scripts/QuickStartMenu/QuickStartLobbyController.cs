using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks {

    [SerializeField]
    private GameObject quickStartButton; // Crete and joining game
    [SerializeField]
    private GameObject quickCancelButton; // Cancel searching for a game to enter
    [SerializeField]
    private int RoomSize; // Max player can join the same room

    // Callback to first connection
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }

    public void QuickStart()
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); // It tries to join an existing room first or calls next method
        Debug.Log("Quick Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room.");
        CreateRoom();
    }

    void CreateRoom() // Create room for ouselves 
    {
        Debug.Log("Creating a room...");
        int randomRoomNumber = Random.Range(0, 10000); // Random room name
        RoomOptions roomOpt = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)RoomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOpt); // Trying to create a new room. Otherwise calls next method
        Debug.Log("Room " + randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Create room failed. Trying again...");
        CreateRoom(); // If it tries to create a room with an existing name, it will fail
        PhotonNetwork.JoinRandomRoom();
    }

    public void QuickCancel()
    {
        Debug.Log("QUICKCANCEL");
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
