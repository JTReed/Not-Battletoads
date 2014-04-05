using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float acceleration;
    public float gravity;
    public float jumpSpeed;
    public Transform punchCollider;

    private Vector3 currentSpeed;
    private Vector3 targetSpeed;
    private Vector3 fwdVector;
    private float punchStartTime;
    private float punchDuration = .4f;

    private bool jump;
    private bool punching;

    private LayerMask attackLayerMask;

    private Transform spriteTransform;
    private CharacterController charControl;
    private Animator animator;

    void Awake()
    {
        attackLayerMask = LayerMask.NameToLayer("Enemy") << 1;
    }

	// Use this for initialization
	void Start () {
        charControl = GetComponent<CharacterController>();
        spriteTransform = transform.FindChild("Sprite");
        animator = spriteTransform.GetComponent<Animator>();
        fwdVector = Vector3.right;
        currentSpeed = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

        //handle jump
        if (charControl.isGrounded) {
            animator.SetBool("Jumping", false);
            currentSpeed.y = 0;
            if (Input.GetButtonDown("Jump")) {
                animator.SetBool("Jumping", true);
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

        // handle punching
        if (!punching) {
            if (Input.GetButtonDown("Punch")) {

                punching = true;
                currentSpeed = Vector3.zero;
                animator.SetBool("Punching", true);
                punchStartTime = Time.time;
            }
        }
        else {
            if (Time.time - punchStartTime >= punchDuration) {
                punching = false;
                animator.SetBool("Punching", false);
            }
            else {
                currentSpeed = Vector3.zero;
                RaycastHit hit;
                Debug.DrawRay(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector * 1.5f, Color.red);
                if (Physics.Raycast(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector, out hit, 1.5f, attackLayerMask)) {
                    Debug.Log("punched enemy yeah!!");
                }
            }
        }

        if (Mathf.Abs(currentSpeed.x) > 0 || Mathf.Abs(currentSpeed.z) > 0) {
            animator.SetFloat("Speed", 1);
        }
        else {
            animator.SetFloat("Speed", 0);
        }

        float facing = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        if (targetSpeed.x != 0) {
            // flip character sprite if going left
            spriteTransform.eulerAngles = (facing < 0) ? (Vector3.up * 180) + (Vector3.right * 315) : Vector3.right * 45;
            fwdVector = (facing < 0) ? Vector3.left : Vector3.right;
        }


        charControl.Move(currentSpeed * Time.deltaTime);

        // handle punching
        if (!punching) {
            if (Input.GetButtonDown("Punch")) {

                punching = true;
                animator.SetBool("Punching", true);
                punchStartTime = Time.time;
            }
        }
        else {
            if (Time.time - punchStartTime >= punchDuration) {
                punching = false;
                animator.SetBool("Punching", false);
            }
            else {
                RaycastHit hit;
                Debug.DrawRay(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector * 1.5f, Color.red);
                if (Physics.Raycast(transform.position + new Vector3(0, transform.position.y / 2.0f, 0), fwdVector, out hit, 1.5f, attackLayerMask)) {
                    Debug.Log("punched enemy yeah!!");
                }
            }
        }
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
