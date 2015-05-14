using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour {
	public enum eScene {
		eTitle,
		eGameMain,
	};

	private static SceneTransition m_instance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static SceneTransition GetInstance () {
		if (m_instance == null) {
			m_instance = new SceneTransition ();
		}

		return m_instance;
	}

	public void TransScene (eScene nextScene) {
		switch (nextScene) {
		case eScene.eTitle:
			Application.LoadLevel ("Title");
			break;
		case eScene.eGameMain:
			Application.LoadLevel ("GameMain");
			break;
		}
	}
}
