using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject enemy;
    private GameCamera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<GameCamera>();
        Object clone = Instantiate(player, new Vector3(0, 5, 0), Quaternion.identity);
        Object Enemy1 = Instantiate(enemy, new Vector3(50, 7, 0), Quaternion.identity);
        Object Enemy2 = Instantiate(enemy, new Vector3(118, 3, 0), Quaternion.identity);
        camera.SetTarget((clone as GameObject).transform);

	}
}
