using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {

	Vector3 startPosition;
	Vector3 startSize;
	
	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
		startSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdateFuelBar(float fuel){
		Vector3 newPosition = startPosition;
		newPosition.x = startPosition.x - (2f * (1 - fuel / 100f));
		transform.localPosition = newPosition;
		
		
		Vector3 newSize = startSize;
		newSize.x = startSize.x - (0.4f * (1 - fuel / 100f));
		transform.localScale = newSize;
		
		
		Vector2 texOffset = new Vector2(fuel / 100f, 1);
		renderer.material.SetTextureScale("_MainTex", texOffset);
		
	}
}
