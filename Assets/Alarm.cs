using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour {
	
	Vector3 initPosition = new Vector3(6, 0, 5);
	
	
	const float blinkingInterval = 0.2f;
	
	
	float lifeTimer = 0;
	
	float blinkingTimer = 0;
	
	
	GameController game;
	
	
	AlarmAudioController alarmAudio;
	
	// Use this for initialization
	void Start () {
		game = Camera.mainCamera.GetComponent<GameController>();
				
		alarmAudio = GameObject.FindGameObjectWithTag("AlarmAudio").GetComponent<AlarmAudioController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		blinkingTimer -= Time.deltaTime;
		
		if (blinkingTimer < 0){
			if (renderer.enabled){
				renderer.enabled = false;	
			}else{
				renderer.enabled = true;		
			}
			
			blinkingTimer = blinkingInterval;
		}
		
		
		
		lifeTimer -= Time.deltaTime;
		
		if (lifeTimer < 0){
			game.SpawnAsteroid(transform.position.y);
			
			GameObject.Destroy(gameObject);	
		}
		
	}
	
	public void Initialize(float y, float duration){
		initPosition.y = y;
		
		transform.position = initPosition;
		
		lifeTimer = duration;
		
		blinkingTimer = blinkingInterval;
		
		
		if (alarmAudio == null) alarmAudio = GameObject.FindGameObjectWithTag("AlarmAudio").GetComponent<AlarmAudioController>();

		
		alarmAudio.PlayAlarm();
		
	}
}
