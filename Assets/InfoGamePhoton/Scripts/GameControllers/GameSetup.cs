using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    public static GameSetup GS;
    
    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }

    /**
    // Use this for initialization
    void Start () {
        CreatePlayer();
	}
	
	public void CreatePlayer()
    {
        Debug.Log("Creating new player...");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }*/

}
