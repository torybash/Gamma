using UnityEngine;
using System.Collections;

public class AlarmAudioController : MonoBehaviour {
	
	public AudioClip alarmSound;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void PlayAlarm(){
		audio.Play();
		//audio.PlayOneShot(alarmSound);
			
	}
}
