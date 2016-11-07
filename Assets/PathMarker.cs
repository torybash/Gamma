using UnityEngine;
using System.Collections;

public class PathMarker : MonoBehaviour {
	
	Vector2 offsetVector = Vector2.zero;
	
	float speed = 0.02f;
	
	Color currentColor;
	
	// Use this for initialization
	void Start () {
		currentColor = new Color(1,1,1,1);
	}
	
	// Update is called once per frame
	void Update () {
		
		currentColor.a = (0.15f * Mathf.Cos(10 *Time.time)) + 0.85f;
		//currentColor.b = (0.1f * Mathf.Cos(Time.deltaTime)) + 0.5f;
				
		renderer.material.color = currentColor;
		
		
		offsetVector.x -= speed * Time.deltaTime;
		
		renderer.material.SetTextureOffset("_MainTex", offsetVector);
	}
}
