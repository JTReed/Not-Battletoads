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


        Vector3 targetSpeed = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        currentSpeed.x = MoveToward(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.z = MoveToward(currentSpeed.z, targetSpeed.z, acceleration);

        //handle gravity
        currentSpeed.y -= gravity;
        //handle jump
        if (charControl.isGrounded && Input.GetButtonDown("Jump"))
            currentSpeed.y += jumpSpeed;

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
