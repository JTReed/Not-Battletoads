using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public float speed;
    public float acceleration;
    public float gravity;
   // public float jumpSpeed;
    private float moodTime;
    private int mood;
    private Vector3 currentSpeed;
    private Vector3 targetSpeed;

    private bool jump;

    private Transform spriteTransform;
    private Transform playerTransform;
    private CharacterController charControl;

	// Use this for initialization
	void Start () {
        charControl = GetComponent<CharacterController>();
        currentSpeed = Vector3.zero;
        spriteTransform = transform.FindChild("Sprite");
        playerTransform = null;
        mood = 0;
        moodTime = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {

        //wait for player spawn to grab player transform
        if(playerTransform == null) {
            playerTransform = GameObject.Find("Player(Clone)").transform;
        }

        //handle (not actually jump)
        if (charControl.isGrounded) {
            currentSpeed.y = 0;
        }
        else {
            //handle gravity
            currentSpeed.y -= gravity * Time.deltaTime;
        }

        Vector3 targetSpeed = Vector3.zero;
        switch(mood) {
            case 0: 
                targetSpeed = new Vector3(-speed, 0, 0);
                //need to check for player character set mood if close
                if(playerTransform != null && (moodTime < 6.0f) && ((playerTransform.position - transform.position).magnitude < 4.0f)) {
                    mood = 2;
                    moodTime = 3.0f;
                    break;
                }
                moodTime -= Time.deltaTime;
                if (moodTime < 0.0f)
                    mood = 1;
                break;
            case 1:
                targetSpeed = new Vector3(speed, 0, 0);
                //need to check for player character set mood if close
                if (playerTransform != null && (moodTime < 6.0f) && ((playerTransform.position - transform.position).magnitude < 4.0f))
                {
                    mood = 2;
                    moodTime = 3.0f;
                    break;
                }
                moodTime -= Time.deltaTime;
                if (moodTime < 0.0f)
                    mood = 1;
                break;
            case 2:
                targetSpeed = (playerTransform.position - transform.position).normalized;
                moodTime -= Time.deltaTime;
                if(moodTime < 0.0f)
                    mood = 0;
                break;
        }
        //Vector3 targetSpeed = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        currentSpeed.x = MoveToward(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.z = MoveToward(currentSpeed.z, targetSpeed.z, acceleration);

        float facing = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        if (targetSpeed.x != 0) {
            // flip character sprite if going left
            spriteTransform.eulerAngles = (facing < 0) ? (Vector3.up * 180) + (Vector3.right * 315) : Vector3.right * 45;
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
