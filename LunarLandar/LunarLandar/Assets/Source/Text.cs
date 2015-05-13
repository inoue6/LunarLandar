using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {
	// GUITextの種類.
	public enum TextType {
		eHorizontalSpeed,
		eVerticalSpeed,
		eFuel,
		eGameOver,
		eGameClear,
	}

	const int defaultWidth = 800;		// 横幅.
	const int defaultHeight = 600;		// 縦幅.
	const int parametersFontSize = 30;	// 垂直、平行のスピードと燃料のフォント.
	const int resultFontSize = 60;

	public TextType m_text;
	public Rocket cRocket;
	public GUIText cText;
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

		if (cRocket == null) {
			return;
		}

		switch (m_text) {
		case TextType.eHorizontalSpeed:
			UpdateText ((int)cRocket.m_horizontalSpeed, "HorizontalSpeed：");
			break;
		case TextType.eVerticalSpeed:
			UpdateText ((int)cRocket.m_verticalSpeed, "VerticalSpeed：");
			break;
		case TextType.eFuel:
			cText.text = "Fuel：" + cRocket.m_fuel.ToString ();
			break;
		case TextType.eGameClear:
			cText.text = "GAME CLEAR";
			break;
		case TextType.eGameOver:
			cText.text = "GAME OVER";
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
		case TextType.eGameClear:
			transform.position = new Vector2 (0.25f, 0.6f);
			break;
		case TextType.eGameOver:
			transform.position = new Vector2 (0.28f, 0.6f);
			break;
		}
	}

	// スピードの符号を反転して表示.
	void UpdateText (int speed, string text) {
		speed *= -1;
		cText.text = text + speed.ToString ();
	}

	// フォントサイズ更新.
	void UpdateFontSize () {
		int baseSize = parametersFontSize;
		if (m_text == TextType.eGameClear || m_text == TextType.eGameOver) {
			baseSize = resultFontSize;
		}
		m_screenSize = new Vector2 (Screen.width, Screen.height);
		float fontSize = baseSize * (m_screenSize.x / defaultWidth);
		if(fontSize > baseSize * (m_screenSize.y / defaultHeight)) {
			fontSize = baseSize * (m_screenSize.y / defaultHeight);
		}
		cText.fontSize = (int)fontSize;
	}
}
