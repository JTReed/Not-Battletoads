using UnityEngine;
using System.Collections;

public class EntityMoveCheck : MonoBehaviour {

    CircleCollider2D entityCollider;

	// Use this for initialization
	void Start () {

        //get the collider
        entityCollider = getComponent<CircleCollider2D>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionStay2D(Collision c)
    {
        //Handle collision with world polygons
        if (c.gameObject.tag == "World")
        {
            //Handle walkables
            if (c.gameObject.walkable)
            {
                transform = currentSpeed.x * c.gameObject.leftRightVec + currentSpeed.y * c.gameObject.upDownVec;
            }
            //Handle not walkables
            else
            {

            }
        }
    }

}
