using UnityEngine;
using System.Collections;

public class FrontMessage : MonoBehaviour {
	
	public enum MessageType{
		OUT_OF_BOUNDS	
	}
	
	MessageType messageType;
	
	Color outOfBoundsColor = new Color(0.95f, 0, 0, 0.7f);
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowMessage(string message, MessageType type){
		messageType = type;
		GetComponent<TextMesh>().text = message;
		
		if (messageType == MessageType.OUT_OF_BOUNDS){
			renderer.material.color = outOfBoundsColor;
		}
	}
	
	
	public void Active(bool active){
		renderer.enabled = active;	
	}
}
