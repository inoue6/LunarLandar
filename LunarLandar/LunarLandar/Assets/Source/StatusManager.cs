using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {
	// 現在の状態.
	enum eStatus {
		eTutorial,
		ePlay,
		eGameOver,
		eGameClear,
	};
	
	eStatus m_status;
	public GameObject cDescription1;
	public GameObject cDescription2;
	public Rocket cRocket;
	public Text cHorizonSpeedText;
	public Text cVerticalSpeedText;
	public Text cFuelText;
	public Text cGameClearText;

	public int m_enterCount;

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
		case eStatus.eGameClear:
			UpdateGameClear ();
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
		case eStatus.eGameClear:
			StartGameClear ();
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
				cDescription1.transform.position = new Vector2 (1000, 1000);
				break;
			case 2:
				cDescription2.transform.position = new Vector2 (1000, 1000);
				Transit (eStatus.ePlay);
				break;
			}
		}
	}

	// ゲームのスタート.
	void StartPlay () {
		cRocket.Initialize ();

		cHorizonSpeedText.SetPosition ();
		cVerticalSpeedText.SetPosition ();
		cFuelText.SetPosition ();
	}

	// ゲームのアップデート.
	void UpdatePlay () {
		if (cRocket.m_fuel > 0) {
			// ロケット操作.
			cRocket.OperationRocket ();
		}
		else {
			// 燃料がなくなったら動けない.
			if(cRocket.m_propulsionFlag) {
				cRocket.m_propulsion.transform.position = new Vector2(1000, 1000);
			}
			cRocket.m_propulsionFlag = false;
		}

		//ロケット更新.
		cRocket.UpdateRocket ();

		// 着地成功.
		if (cRocket.m_landing && cRocket.CheckClear ()) {
			Transit (eStatus.eGameClear);
		}
		// 着地失敗.
		if (cRocket.m_forcedLanding) {
			Transit (eStatus.eGameOver);
		}
	}

	// ゲームクリアのスタート.
	void StartGameClear () {
		Destroy (cRocket.m_propulsion);
		cGameClearText.SetPosition ();
	}

	// ゲームクリアのアップデート.
	void UpdateGameClear () {

	}

	// ゲームオーバーのスタート.
	void StartGameOver () {
		cRocket.Explosion ();
		Destroy (cRocket.m_rocket);
		Destroy (cRocket.m_propulsion);
	}

	// ゲームオーバーのアップデート.
	void UpdateGameOver () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			// シーン遷移.
		}
	}
}
