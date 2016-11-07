using UnityEngine;
using System.Collections;

public class SpeedCounter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public void UpdateSpeedCounter(float speed){
		GetComponent<TextMesh>().text = ""+speed + "c";
	}
}
