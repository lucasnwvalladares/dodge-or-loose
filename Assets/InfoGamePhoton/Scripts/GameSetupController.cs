using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CreatePlayer();
	}
	
	public void CreatePlayer()
    {
        Debug.Log("Creating new player...");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }
}
