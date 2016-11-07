using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	
	public enum PowerUpType{
		FUEL,
		SPEED
	}
	
	Vector3 initPosition = new Vector3(7, 0, 5);
	
	
	float speed = 0;
	
	PowerUpType type;
	
	
	GameController game;
	
	// Use this for initialization
	void Start () {
		game = Camera.mainCamera.GetComponent<GameController>();
		
		speed = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
	
		
		
		if (transform.position.x < -10){
			GameObject.Destroy(gameObject);	
		}
	}
	
	public void Initialize(float y, PowerUpType type){
		this.type = type;
		
		initPosition.y = y;
		
		transform.position = initPosition;
	}
	
	
    void OnTriggerEnter(Collider other) {
		
		game.PickedUpPowerUp(type, transform.position);
		
        Destroy(gameObject);
    }

}
