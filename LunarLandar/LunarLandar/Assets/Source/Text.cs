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
		eDegree,
	}

	// 定数
	public static readonly Color TEXT_RED = new Color (1.0f, 0.0f, 0.0f);		// テキストカラー赤
	public static readonly Color TEXT_GREEN = new Color (0.0f, 1.0f, 0.0f);		// テキストカラー緑


	const int defaultWidth = 800;		// 横幅.
	const int defaultHeight = 600;		// 縦幅.
	const int parametersFontSize = 30;	// 垂直、平行のスピードと燃料のフォント.
	const int resultFontSize = 60;
	const int titleFontSize = 60;

	public TextType m_text;
	public Rocket m_rocket;
	public GUIText m_guiText;
	Vector2 m_screenSize;

	public string decision;

	// Use this for initialization
	void Start () {
		m_screenSize = new Vector2 (defaultWidth, defaultHeight);
		decision = "NG";
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
		case TextType.eDegree:
			m_guiText.text = "Degree："+decision.ToString();
			break;
		}

		CheckClear (m_rocket.m_horizontalSpeed,m_rocket.m_verticalSpeed,m_rocket.m_fuel);

		// ロケットの傾きから着陸可能かどうかテキストに反映する
		if (m_rocket.m_rotationAngle==0) {
			SetColor ("DegreeText", TEXT_GREEN);
			decision = "OK";
		}
		else{
			SetColor ("DegreeText", TEXT_RED);
			decision = "NG";
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
		case TextType.eDegree:
			transform.position = new Vector2(0.715f,0.75f);
			break;
		}
	}

	// 着陸条件を満たした場合テキストの色を緑へ
	// それ以外は赤色に設定する関数
	public void CheckClear(float m_horizontalSpeed, float m_verticalSpeed, float m_fuel){
		if ((int)m_horizontalSpeed >= -100 && (int)m_horizontalSpeed <= 100) {
			SetColor ("HorizontalSpeedText", TEXT_GREEN);
		}
		else {
			SetColor ("HorizontalSpeedText", TEXT_RED);
		}

		if ((int)m_verticalSpeed >= 0 && (int)m_verticalSpeed <= 100) {
			SetColor ("VerticalSpeedText", TEXT_GREEN);
		}
		else {
			SetColor ("VerticalSpeedText", TEXT_RED);
		}

		if ((int)m_fuel > 0) {
			SetColor ("FuelText", TEXT_GREEN);
		}
		else {
			SetColor ("FuelText", TEXT_RED);
		}
	}

	// テキストを指定した色に変更する関数
	public void SetColor(string textName,Color color){
		GUIText text = GameObject.Find (textName).GetComponent<GUIText>();
		text.color = color;
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
