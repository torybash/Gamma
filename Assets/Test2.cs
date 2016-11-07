using UnityEngine;
using System.Collections;

public class Test2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate(-Time.deltaTime, 0, 0);
		}else if (Input.GetKey(KeyCode.RightArrow)){
			transform.Translate(Time.deltaTime, 0, 0);
		}
	}
	
	
}
