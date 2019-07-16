using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviourPun, IPunObservable
{

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
    }
	
	// Update is called once per frame
	private void Update () {
        if (this.photonView.IsMine)
        {
            targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
            currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

            amountToMove.y = 0;

            if (Input.GetKey(KeyCode.W))
            {
                amountToMove.y = jumpHeight;
            }

            amountToMove.x = currentSpeed;
            amountToMove.y -= gravity * Time.deltaTime;
            playerPhysics.Move(amountToMove * Time.deltaTime);
        }
    }

    // Increase n towars target by speed
    private float IncrementTowards(float n, float target, float a)
    {
        if(n == target)
        {
            return n;
        } else {
            float dir = Mathf.Sign(target - n); // increase or decrease to get closer to target
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n))? n: target; // if n passes target then return target, otherwise return n
        }
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
    }
}
