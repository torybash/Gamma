using UnityEngine;
using System.Collections;

public class Mars : MonoBehaviour {

	
	Vector3 initPosition = new Vector3(10.5f, 0, 5.5f);
	
	float speed = 1.2f;
	
	GameController game;
	
	bool moving = true;
	
	// Use this for initialization
	void Start () {
		game = Camera.mainCamera.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!moving) return;
		
		transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
		
		
		if (transform.position.x < 0){
			moving = false;
		}
		
//		if (transform.position.x < -10){
//			GameObject.Destroy(gameObject);	
//		}
	}
	
	
	public void Initialize(){		
		transform.position = initPosition;
	}
	
	
    void OnTriggerEnter(Collider other) {
		game.MadeItToMars();
	}
}
