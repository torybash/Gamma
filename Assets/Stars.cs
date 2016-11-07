using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
	
	const float speed = 0.6f;
	
	Vector2 offsetVector = Vector2.zero;
	
	Vector3 startScale;
	
	
	
	// Use this for initialization
	void Start () {
		startScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		offsetVector.x -= speed * Time.deltaTime;
		
		renderer.material.SetTextureOffset("_MainTex", offsetVector);
	}


	public void UpdateScale(float gamma){
		Vector3 newScale = startScale;
		newScale.x *= gamma;
		transform.localScale = newScale;
	}

}
