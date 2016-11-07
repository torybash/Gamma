using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {
	
	string timeString = "";
	string speedString = "";
	
	//TEMP
	string powerString = "";
	string gammaString = "";
		
	ArrayList currentAlarms = new ArrayList();
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI () {
		
//		GUI.TextArea(new Rect(5, 5, 100, 30), timeString);
//		GUI.TextArea(new Rect(5, 40, 100, 30), speedString);
//		GUI.TextArea(new Rect(5, 75, 100, 30), powerString);
//		GUI.TextArea(new Rect(5, 110, 100, 30), gammaString);
//		GUI.TextArea(new Rect(Screen.width - 105, 5, 100, 30), ""+ (1f / Time.deltaTime));
		
		
//		// Make a background box
//		GUI.Box(new Rect(10,10,100,90), "Loader Menu");
//
//		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
//		if(GUI.Button(new Rect(20,40,80,20), "Level 1")) {
//			Application.LoadLevel(1);
//		}
//
//		// Make the second button.
//		if(GUI.Button(new Rect(20,70,80,20), "Level 2")) {
//			Application.LoadLevel(2);
//		}
	}
	
	
	public void UpdateGUI(float time, float speed, float fuel, float gamma){
		int msec = (int) (time * 10000) % 10000;
		
		timeString = "0";
		timeString += (int) time;
		timeString += ":";
		if (msec < 1000) timeString += "0";
		if (msec < 100) timeString += "0";
		if (msec < 10) timeString += "0";
		timeString += "" + msec;
		
		speedString = "" + speed + "c";
		
		powerString = "" + fuel;
		
		gammaString = "" +  1f / gamma;
	}
	
	


}

