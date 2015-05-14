using UnityEngine;
using System.Collections;

public class TransitGameMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneTransition.GetInstance ().TransScene (SceneTransition.eScene.eGameMain);
		}
	}
}
