using Photon.Pun;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks {

	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
    {
        // base.OnConnectedToMaster();
        Debug.Log("Connected to " + PhotonNetwork.CloudRegion + " server!");
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
