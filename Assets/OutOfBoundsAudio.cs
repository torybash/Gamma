using UnityEngine;
using System.Collections;

public class OutOfBoundsAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void PlayOOBSound(){
		if (!audio.isPlaying)
		audio.Play();
	}
	
	public void StopPlayingOOBSound(){
		audio.Stop();
	}

}
