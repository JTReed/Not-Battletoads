using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
    public float speed;
    public float acceleration;

    private Vector2 currentSpeed;
    private Vector2 targetSpeed;

    // private floor collider?

	// Use this for initialization
	void Start () {
        currentSpeed = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {

        Vector2 targetSpeed = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
        currentSpeed.x = MoveToward(currentSpeed.x, targetSpeed.x, acceleration);
        currentSpeed.y = MoveToward(currentSpeed.y, targetSpeed.y, acceleration);

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
