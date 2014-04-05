using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Transform target;
    public float minX;
    public float maxX;
    public bool locked;
	// Use this for initialization
	void Start () {
        locked = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//do cam movement late!
	void LateUpdate(){
        if (target != null) {
            if (!locked)
            {
                //set camera for player's X position, keep previous y/z position. (with min/max X position)
                if (target.position.x < minX)
                    transform.position = new Vector3(minX, transform.position.y, transform.position.z);
                else if (target.position.x > maxX)
                    transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
            }
        }
	}

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
