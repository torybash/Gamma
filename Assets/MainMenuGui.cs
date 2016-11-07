using UnityEngine;
using System.Collections;

public class MainMenuGui : MonoBehaviour {

	
	public Texture buttonPlayTexture;
	public Texture buttonHowToTexture;
	
	GUIStyle style1 = new GUIStyle();
	GUIStyle style2 = new GUIStyle();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
    void OnGUI() {
		     
		
		if (GUI.Button(new Rect(Screen.width / 2f - (0.13f * Screen.width), Screen.height - 0.18f * Screen.height, 0.24f * Screen.width, 0.24f * Screen.height), buttonHowToTexture, style2)){
			Application.LoadLevel("HowTo");
		}
        if (GUI.Button(new Rect(Screen.width / 2f - (0.13f * Screen.width), Screen.height - 0.3f * Screen.height, 0.24f * Screen.width, 0.24f * Screen.height), buttonPlayTexture, style1)){
            Application.LoadLevel("Scene");
		}
			
			

    }
}
