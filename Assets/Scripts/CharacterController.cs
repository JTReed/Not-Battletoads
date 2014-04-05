using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public float speed;
    public float acceleration;

    private Vector3 currentSpeed;
    private Vector3 targetSpeed;

    private bool jump;

    // private floor collider?

	// Use this for initialization
	void Start () {
        currentSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 targetSpeed = new Vector3(Input.GetAxisRaw("Horizontal") * speed, 0, Input.GetAxisRaw("Vertical") * speed);
        currentSpeed.x = MoveToward(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.z = MoveToward(currentSpeed.z, targetSpeed.z, acceleration);

        transform.Translate(currentSpeed);
	}

    float MoveToward(float curr, float targ, float accel)
    {
        if (curr == targ) {
            return curr;
        }

        float direction = Mathf.Sign(targ - curr);
        curr += accel * direction * Time.deltaTime;
        // return the target if curr has passed it
        return (Mathf.Sign(curr - targ) == direction) ? curr : targ;
    }

}