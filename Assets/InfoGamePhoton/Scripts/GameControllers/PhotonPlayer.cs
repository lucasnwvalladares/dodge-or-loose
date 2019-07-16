using System;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonAvatar"), 
                                      GameSetup.GS.spawnPoints[spawnPicker].position, 
                                      GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);

        }
    }
}
