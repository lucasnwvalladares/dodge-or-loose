using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetup : MonoBehaviour {

    public static GameSetup GS;

    public Transform[] spawnPoints;
    
    private void OnEnable()
    {
        if (GameSetup.GS == null)
        {
            GameSetup.GS = this;
        }
    }
}
