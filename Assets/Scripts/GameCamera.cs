using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//do cam movement late!
	void LateUpdate(){
        if (target != null) {
            //set camera for player's X position, keep previous y/z position.
            transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
        }
	}

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
