using UnityEngine;
using System.Collections;

public static class Gamma {
	
	
	public static float GetGamma (float currentSpeed) {
		
		float gamma = Mathf.Sqrt(1 - (currentSpeed * currentSpeed));
		
		
		
		return gamma;
	}
}
