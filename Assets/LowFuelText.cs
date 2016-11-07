using UnityEngine;
using System.Collections;

public class LowFuelText : MonoBehaviour {

	bool showingText = false;
	
	bool blinking = false;
	float blinkingTimer = 0;
	float blinkingInterval = 0.15f;
	
	// Use this for initialization
	void Start () {
		renderer.material.color = Color.red;
		
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (showingText){
			blinkingTimer -= Time.deltaTime;
			
			if (blinkingTimer < 0){
				if (blinking){
					blinking = false;
					renderer.enabled = true;
					blinkingTimer = blinkingInterval;	
				}else{
					blinking = true;
					renderer.enabled = false;
					blinkingTimer = blinkingInterval;	
				}
			}
		}
	}
	
	
	public void ShowText(){
		if (showingText) return;
		
		renderer.enabled = true;
		
		showingText = true;
	}
	
	public void HideText(){
		renderer.enabled = false;
		
		showingText = false;
	}
}
