﻿using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {
	// 現在の状態.
	enum eStatus {
		eTutorial,
		ePlay,
		eGameOver,
		eStageClear,
	};
	
	eStatus m_status;
	public GameObject m_description1;
	public GameObject m_description2;
	public Rocket m_rocket;
	public CameraControl m_camera;
	public FuelMeter m_fuelMeter;
	public BackGround m_backGround1;
	public BackGround m_backGround2;
	public Score m_score;
	public Stage m_stageNow;
	public Stage m_stageNext;
	public Text m_horizonSpeedText;
	public Text m_verticalSpeedText;
	public Text m_fuelText;
	public Text m_stageClearText;
	public Text m_stageReachingNumText;
	public Text m_horizontalText;
	public Text m_missLandingText;
	public Text m_scoreText;
	public Text m_timeText;
	public int m_enterCount;
	public Timer m_timer;
	bool m_firstClick;

	// Use this for initialization
	void Start () {
		// チュートリアルセット.
		Transit (eStatus.eTutorial);
	}
	
	// Update is called once per frame
	void Update () {
		switch (m_status) {
		case eStatus.eTutorial:
			UpdateTutorial ();
			break;
		case eStatus.ePlay:
			UpdatePlay ();
			break;
		case eStatus.eStageClear:
			UpdateStageClear ();
			break;
		case eStatus.eGameOver:
			UpdateGameOver ();
			break;
		}
	}

	// 次のステータスの準備.
	void Transit (eStatus nextStatus) {
		switch (nextStatus) {
		case eStatus.eTutorial:
			StartTutorial ();
			break;
		case eStatus.ePlay:
			StartPlay ();
			break;
		case eStatus.eStageClear:
			StartStageClear ();
			break;
		case eStatus.eGameOver:
			StartGameOver ();
			break;
		}
		
		m_status = nextStatus;
	}

	// チュートリアルのスタート.
	void StartTutorial () {
		m_enterCount = 0;
	}

	// チュートリアルのアップデート.
	void UpdateTutorial () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			m_enterCount++;
			switch (m_enterCount) {
			case 1:
				m_description1.transform.position = new Vector2 (1000, 1000);
				break;
			case 2:
				m_description2.transform.position = new Vector2 (1000, 1000);
				Transit (eStatus.ePlay);
				break;
			}
		}
	}

	// ゲームのスタート.
	void StartPlay () {
		m_rocket.Initialize ();
		m_stageNow.Initialize ();
		m_stageNext.Initialize ();
		m_score.Initialize ();
		m_backGround1.Initialize ();
		m_backGround2.Initialize ();

		m_horizonSpeedText.SetPosition ();
		m_verticalSpeedText.SetPosition ();
		m_fuelText.SetPosition ();
		m_stageReachingNumText.SetPosition ();
		m_horizontalText.SetPosition ();;
		m_missLandingText.SetPosition ();
		m_scoreText.SetPosition ();
		m_timeText.SetPosition ();

		m_fuelMeter.SetPosition ();
		
		m_timer.time = 0;
	}

	// ゲームのアップデート.
	void UpdatePlay () {
		// ステージをセットする
		m_stageNow.SetStage ();
		m_stageNext.SetStage();

		m_backGround1.SetBackGround ();
		m_backGround2.SetBackGround ();

		// ゲームプレイ時間の計測
		m_timer.time += Time.deltaTime;
		m_score.SetTime (m_timer.time);

		if (m_rocket.m_fuel > 0) {
			// ロケット操作.
			m_rocket.OperationRocket ();
		}
		else {
			// 燃料がなくなったら動けない.
			if(m_rocket.m_propulsionFlag) {
				m_rocket.m_propulsion.transform.position = new Vector2(1000, 1000);
			}
			m_rocket.m_propulsionFlag = false;
		}

		//ロケット更新.
		m_rocket.UpdateRocket ();

		if (m_rocket.m_landing) {
			if(m_rocket.CheckClear ()) {
				// スコアの計算
				m_score.ComputeScore();
				// 着地成功.
				Transit (eStatus.eStageClear);
			}
			else {
				// 着地失敗.
				Transit (eStatus.eGameOver);
			}
		}
		if (m_rocket.m_forcedLanding) {
			// 着地失敗.
			Transit (eStatus.eGameOver);
		}

		m_fuelMeter.SetPosition ();
		m_fuelMeter.MeterColorChange ();
	}

	// ステージクリアのスタート.
	void StartStageClear () {
		m_rocket.m_propulsion.transform.position = new Vector2 (1000, 1000);
		m_stageClearText.SetPosition ();
	}

	// ステージクリアのアップデート.
	void UpdateStageClear () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			// ステージ切り替え.
			m_stageClearText.HideText ();
			m_rocket.NextStageInitialize ();
			m_backGround1.Initialize();
			m_backGround2.Initialize();
			m_camera.Initialize ();

			m_status = eStatus.ePlay;
			m_timer.time = 0;
		}
	}

	// ゲームオーバーのスタート.
	void StartGameOver () {
		m_rocket.Explosion ();
		Destroy (m_rocket.m_rocket);
		Destroy (m_rocket.m_propulsion);
		m_firstClick = true;
	}

	// ゲームオーバーのアップデート.
	void UpdateGameOver () {
		if (Input.GetKeyDown (KeyCode.Return) && m_firstClick) {
			m_firstClick = false;
			// シーン遷移.
			SceneTransition.GetInstance ().TransScene (SceneTransition.eScene.eTitle);
		}
	}
}
