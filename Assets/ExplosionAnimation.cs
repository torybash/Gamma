using UnityEngine;
using System.Collections;

public class ExplosionAnimation : MonoBehaviour {
	
	const float frameInterval = 0.15f;
	
	bool animationRunning = false;
	
	float offset = 0;
	
	float timer = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.B)){
//			RunAnimation();
//		}
		
		if (animationRunning){
			if (offset > 0.751f){ animationRunning = false; return;}
			
			timer -= Time.deltaTime;
			
			if (timer < 0){
				timer = frameInterval;
				
				offset += 0.125f;
				
				renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0.5f));
			}
		}
		
	}
	
	public void RunAnimation(){
		animationRunning = true;
		
		timer = frameInterval;
	}
}
