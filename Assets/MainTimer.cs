using UnityEngine;
using System.Collections;

public class MainTimer : MonoBehaviour {

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void UpdateTimer(float time){
		int msec = (int) (time * 100) % 100;
		
		string timeString = "0";
		timeString += (int) time;
		timeString += ":";
		if (msec < 10) timeString += "0";
		timeString += "" + msec;
		
		GetComponent<TextMesh>().text = timeString;
	}
}
