using System;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonPlayer : MonoBehaviourPunCallbacks, IPunObservable
{

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;

    public void Awake()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start() {
        CameraWork _cameraWork = gameObject.GetComponent<CameraWork>();

        if (_cameraWork != null)
        {
            if (photonView.IsMine)
            {
                _cameraWork.OnStartFollowing();
            }
        }
        else
        {
            Debug.LogError("<Color=Red><b>Missing</b></Color> CameraWork Component on player Prefab.", this);
        }

        // Create the UI
        if (this.playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(this.playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            this.ProcessInputs();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }

    void OnLevelWasLoaded(int level)
    {
        this.CalledOnLevelWasLoaded(level);
    }

    private void CalledOnLevelWasLoaded(int level)
    {
        if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
        {
            transform.position = new Vector3(0f, 5f, 0f);
        }

        GameObject _uiGo = Instantiate(this.playerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }

    private void ProcessInputs()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadingMode)
    {
        this.CalledOnLevelWasLoaded(scene.buildIndex);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
}
