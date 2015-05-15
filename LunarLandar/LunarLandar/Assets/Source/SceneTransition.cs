using UnityEngine;
using System.Collections;

public class SceneTransition : MonoBehaviour {
	// シーン.
	public enum eScene {
		eTitle,
		eGameMain,
	};

	// 状態.
	enum eStatus {
		eFadeOut,
		eTransScene,
		eFadeIn,
		eNothingIsDone,
	};
	
	private static SceneTransition s_instance;
	eStatus m_status = eStatus.eNothingIsDone;
	eScene m_nextScene;
	public Color m_fadeColor;
	float m_time;
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
		m_fadeColor = Color.black;
		m_fadeColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_status) {
		case eStatus.eNothingIsDone:
			break;
		case eStatus.eFadeOut:
			UpdateFadeOut ();
			break;
		case eStatus.eTransScene:
			break;
		case eStatus.eFadeIn:
			UpdateFadeIn ();
			break;
		}
	}

	// シーン切り替え.
	void Transit (eStatus nextStatus) {
		switch (nextStatus) {
		case eStatus.eNothingIsDone:
			StartNothingIsDone ();
			break;
		case eStatus.eFadeOut:
			StartFadeOut ();
			break;
		case eStatus.eTransScene:
			StartTransScene ();
			break;
		case eStatus.eFadeIn:
			StartFadeIn ();
			break;
		}
	}

	// インスタンスの取得.
	public static SceneTransition GetInstance () {
		if (s_instance == null) {
			GameObject gameObject = new GameObject ("SceneTransition");
			s_instance = gameObject.AddComponent<SceneTransition> ();
		}
		
		return s_instance;
	}

	// 切り替え先のシーンをセット.
	public void TransScene (eScene nextScene) {
		m_nextScene = nextScene;
		Transit (eStatus.eFadeOut);
	}
	
	// 何もしないのスタート.
	void StartNothingIsDone () {
		m_status = eStatus.eNothingIsDone;
	}
	
	// シーン切り替えのスタート.
	void StartTransScene () {
		switch (m_nextScene) {
		case eScene.eTitle:
			Application.LoadLevel ("Title");
			break;
		case eScene.eGameMain:
			Application.LoadLevel ("GameMain");
			break;
		}
		
		Transit (eStatus.eFadeIn);
	}
	
	// フェードアウトのスタート.
	void StartFadeOut () {
		m_status = eStatus.eFadeOut;
		m_time = 0;
	}
	
	// フェードアウトのアップデート.
	void UpdateFadeOut () {
		if (m_time <= 1) {
			m_fadeColor.a = Mathf.Lerp (0, 1, m_time / 1);
			m_time += Time.deltaTime;
		}
		else {
			Transit (eStatus.eTransScene);
		}
	}
	
	// フェードインのスタート.
	void StartFadeIn () {
		m_status = eStatus.eFadeIn;
		m_time = 0;
	}
	
	// フェードインのアップデート.
	void UpdateFadeIn () {
		if (m_time <= 1) {
			m_fadeColor.a = Mathf.Lerp (1, 0, m_time / 1);
			m_time += Time.deltaTime;
		}
		else {
			Transit (eStatus.eNothingIsDone);
			Destroy (GameObject.Find ("SceneTransition"));
		}
	}

	// フェードイン、アウト用のテクスチャーの作成.
	public void OnGUI () {
		GUI.color = this.m_fadeColor;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
	}
}