using UnityEngine;
using System.Collections;

public class TransitGameMain : MonoBehaviour {
	bool m_firstClick;

	// Use this for initialization
	void Start () {
		m_firstClick = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) && m_firstClick) {
			m_firstClick = false;

			// シーン遷移.
			SceneTransition.GetInstance ().TransScene (SceneTransition.eScene.eGameMain);
		}
	}
}
