using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	
	Vector3 initPosition = new Vector3(7, 0, 5);
	
	
	float speed = 0;
	
	GameController game;
	
	// Use this for initialization
	void Start () {
		game = Camera.mainCamera.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
		
		
		
		if (transform.position.x < -10){
			GameObject.Destroy(gameObject);	
		}
	}
	
	
	public void Initialize(float y, float speed){
		this.speed = speed;
		
		
		
		initPosition.y = y;
		transform.position = initPosition;
	}
	
	
    void OnTriggerEnter(Collider other) {
		game.HitByAsteroid();
	}
}
