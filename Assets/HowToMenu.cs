using UnityEngine;
using System.Collections;

public class HowToMenu : MonoBehaviour {

	public Texture buttonNextTexture;
	
	public Texture page2Texture;
	public Texture page3Texture;

	
	HowToPlane howToPlane;
	
	GUIStyle style = new GUIStyle();
	
	int page = 1;
	
	// Use this for initialization
	void Start () {
		howToPlane = GetComponentInChildren<HowToPlane>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
    void OnGUI() {

        if (GUI.Button(new Rect(Screen.width - (0.26f * Screen.width), Screen.height - 0.13f * Screen.height, 0.24f * Screen.width, 0.24f * Screen.height), buttonNextTexture, style)){
			if (page == 1){
				howToPlane.renderer.material.mainTexture = page2Texture;
				page = 2;
			}else if (page == 2){
				howToPlane.renderer.material.mainTexture = page3Texture;
				page = 3;
			}else if (page == 3){
				Application.LoadLevel("MenuScene");
			}
		}
            

        
    }
}
