using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {
	// GUITextの種類.
	public enum TextType {
		eHorizontalSpeed,
		eVerticalSpeed,
		eFuel,
		eStageReachingNum,
		eGameOver,
		eStageClear,
		ePushEnter,
		eTitle,
	}

	const int defaultWidth = 800;		// 横幅.
	const int defaultHeight = 600;		// 縦幅.
	const int parametersFontSize = 30;	// 垂直、平行のスピードと燃料のフォント.
	const int resultFontSize = 60;
	const int titleFontSize = 60;

	public TextType m_text;
	public Rocket m_rocket;
	public GUIText m_guiText;
	Vector2 m_screenSize;
	
	// Use this for initialization
	void Start () {
		m_screenSize = new Vector2 (defaultWidth, defaultHeight);
	}
	
	// Update is called once per frame
	void Update () {
		// フォントサイズ更新.
		if (m_screenSize != new Vector2 (Screen.width, Screen.height)) {
			UpdateFontSize ();
		}

		if (m_rocket == null) {
			return;
		}

		switch (m_text) {
		case TextType.eHorizontalSpeed:
			UpdateText ((int)m_rocket.m_horizontalSpeed, "HorizontalSpeed：");
			break;
		case TextType.eVerticalSpeed:
			UpdateText ((int)m_rocket.m_verticalSpeed, "VerticalSpeed：");
			break;
		case TextType.eFuel:
			m_guiText.text = "Fuel：" + m_rocket.m_fuel.ToString ();
			break;
		case TextType.eStageClear:
			m_guiText.text = "STAGE CLEAR";
			break;
		case TextType.eGameOver:
			m_guiText.text = "GAME OVER";
			break;
		case TextType.eStageReachingNum:
			m_guiText.text = "Stage" + m_rocket.m_stageReachingNum.ToString ();
			break;
		}
	}

	// 座標の指定.
	public void SetPosition () {
		switch (m_text) {
		case TextType.eHorizontalSpeed:
			transform.position = new Vector2 (0.555f, 0.95f);
			break;
		case TextType.eVerticalSpeed:
			transform.position = new Vector2 (0.6f, 0.88f);
			break;
		case TextType.eFuel:
			transform.position = new Vector2 (0.762f, 0.81f);
			break;
		case TextType.eStageClear:
			transform.position = new Vector2 (0.25f, 0.6f);
			break;
		case TextType.eGameOver:
			transform.position = new Vector2 (0.28f, 0.6f);
			break;
		case TextType.eStageReachingNum:
			transform.position = new Vector2 (0.05f, 0.95f);
			break;
		}
	}

	// 非表示.
	public void HideText () {
		transform.position = new Vector2 (1000, 1000);
	}

	// スピードの符号を反転して表示.
	void UpdateText (int speed, string text) {
		speed *= -1;
		m_guiText.text = text + speed.ToString ();
	}

	// フォントサイズ更新.
	void UpdateFontSize () {
		int baseSize = parametersFontSize;
		if (m_text == TextType.eStageClear || m_text == TextType.eGameOver) {
			baseSize = resultFontSize;
		}
		if (m_text == TextType.ePushEnter || m_text == TextType.eTitle) {
			baseSize = titleFontSize;
		}

		m_screenSize = new Vector2 (Screen.width, Screen.height);
		float fontSize = baseSize * (m_screenSize.x / defaultWidth);
		if(fontSize > baseSize * (m_screenSize.y / defaultHeight)) {
			fontSize = baseSize * (m_screenSize.y / defaultHeight);
		}
		m_guiText.fontSize = (int)fontSize;
	}
}
