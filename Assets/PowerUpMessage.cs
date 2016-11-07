using UnityEngine;
using System.Collections;

public class PowerUpMessage : MonoBehaviour {
	
	float lifeTimeDuration = 1.5f;
	
	float lifeTimer = 0;
	
	float moveSpeed = 0.2f;
	
	Color currentColor;
	
	// Use this for initialization
	void Start () {
		lifeTimer = lifeTimeDuration;
		
		currentColor = new Color(1, 1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate(0, moveSpeed * Time.deltaTime, 0, Space.World);
		
		
		lifeTimer -= Time.deltaTime;
		
		
		
		if (lifeTimer < lifeTimeDuration / 2f){			
			currentColor.a = lifeTimer / (lifeTimeDuration / 2f);			
			renderer.material.color = currentColor;
		}
		
		if (lifeTimer < 0){
			GameObject.Destroy(gameObject);
		}
	}
	
	public void Initialize(string message, Vector3 position){
		GetComponent<TextMesh>().text = message;
		
		transform.position = position;
	}
}
