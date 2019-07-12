using System;
using Photon.Pun;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour, Photon.Pun.IPunObservable
{

    private PhotonView PV;

    // Player Movement
    public float moveSpeed = 100f;
    public float jumpForce = 800f;

    private Vector3 selfPosition;

    // Player Handling
    public float gravity = 20;
    public float speed = 12;
    public float acceleration = 35;
    public float jumpHeight = 12;

    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amountToMove;

    private PlayerPhysics playerPhysics;

    // Use this for initialization
    void Start () {
        playerPhysics = GetComponent<PlayerPhysics>();

        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            PV.RPC("RPC_DontDestroy", RpcTarget.All);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (PV.IsMine)
        {
            PV.RPC("RPC_DontDestroy", RpcTarget.All);
            CheckInput();
        }
        else
        {
            SmoothNetMovement();
        }
    }

    [PunRPC]
    public void RPC_DontDestroy()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void CheckInput()
    {
        targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

        if (playerPhysics.grounded)
        {
            amountToMove.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                amountToMove.y = jumpHeight;
            }
        }

        amountToMove.x = currentSpeed;
        amountToMove.y -= gravity * Time.deltaTime;
        playerPhysics.Move(amountToMove * Time.deltaTime);
    }

    // Increase n towars target by speed
    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n); // increase or decrease to get closer to target
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target; // if n passes target then return target, otherwise return n
        }
    }

    private void SmoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPosition, Time.deltaTime * 10);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            selfPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
