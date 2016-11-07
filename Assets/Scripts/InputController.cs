using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	StarshipController ship;
	
	// Use this for initialization
	void Start () {
		ship = GetComponentInChildren<StarshipController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)){
			ship.UpPressed();
		}
		
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)){
			ship.DownPressed();
		}
		
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space)){
			ship.RightPressed();
		}
		
	}
}
