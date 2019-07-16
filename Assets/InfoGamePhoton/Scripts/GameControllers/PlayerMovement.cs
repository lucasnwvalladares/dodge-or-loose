using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private PhotonView PV;
    private CharacterController myCC;

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
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
        playerPhysics = GetComponent<PlayerPhysics>();
    }
	
	// Update is called once per frame
	void Update () {
		if (PV.IsMine)
        {
            BasicMovement();
        }
	}

    void BasicMovement()
    {
        targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

        if (playerPhysics.grounded)
        {
            Debug.Log("CHAO");
            amountToMove.y = 0;

            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("AAAAAAAAAAAA");
                amountToMove.y = jumpHeight;
            }
        }

        amountToMove.x = currentSpeed;
        amountToMove.y -= gravity * Time.deltaTime;
        myCC.Move(amountToMove * Time.deltaTime);
    }

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
}
