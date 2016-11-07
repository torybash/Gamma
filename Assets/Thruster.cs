using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {
	
	public enum Type{
		UP,
		RIGHT
	}
	
	public Type type;
	
	bool turnedOn = false;
	
	public Texture blueUpFlame;
	public Texture blueRightFlame;
	
	Texture originalTexture;
	
	// Use this for initialization
	void Start () {
		originalTexture = renderer.material.mainTexture;
	}
	
	// Update is called once per frame
	void Update () {
	
		
		
		if (turnedOn){
			renderer.enabled = true;	
		}else{
			renderer.enabled = false;	
		}
		
		
		turnedOn = false;
	}
	
	
	
	public void On(){
		turnedOn = true;
	}
	
	public void TurboOn(){
		
		switch (type) {
			
		case Type.UP:
			renderer.material.mainTexture = blueUpFlame;
			break;
			
		case Type.RIGHT:
			renderer.material.mainTexture = blueRightFlame;
			break;
		}
		
	}
	
	
	public void TurboOff(){
		renderer.material.mainTexture = originalTexture;
	}
}
