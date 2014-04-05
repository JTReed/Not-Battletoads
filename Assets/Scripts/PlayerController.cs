using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float acceleration;
    public float gravity;
    public float jumpSpeed;

    private Vector3 currentSpeed;
    private Vector3 targetSpeed;
    private CharacterController charControl;
    private bool jump;

	// Use this for initialization
	void Start () {
        charControl = GetComponent<CharacterController>();
        currentSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

        //handle jump
        if (charControl.isGrounded) {
            currentSpeed.y = 0;
            if (Input.GetButtonDown("Jump")) {
                currentSpeed.y += jumpSpeed;
            }
        }
        else {
            //handle gravity
            currentSpeed.y -= gravity * Time.deltaTime;
        }

        Vector3 targetSpeed = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        currentSpeed.x = MoveToward(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.z = MoveToward(currentSpeed.z, targetSpeed.z, acceleration);

        float facing = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        if (targetSpeed.x != 0) {
            // flip character sprite if going left
            transform.eulerAngles = (facing < 0) ? (Vector3.up * 180) + (Vector3.right * 270) : Vector3.zero;
        }


        charControl.Move(currentSpeed * Time.deltaTime);
	}

    float MoveToward(float curr, float targ, float accel)
    {
        if (curr == targ) {
            return curr;
        }

        float direction = Mathf.Sign(targ - curr);
        curr += accel * direction;
        // return the target if curr has passed it
        return (Mathf.Sign(curr - targ) == direction) ? curr : targ;
    }

}
