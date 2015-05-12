using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {
	enum eStatus {
		eTutorial,
		ePlay,
		eGameOver,
		eGameClear,
	};

	public GameObject description;
	eStatus m_status;
	public Rocket cRocket;

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

	}

	// チュートリアルのアップデート.
	void UpdateTutorial () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			Transit (eStatus.ePlay);
			description.transform.position = new Vector2 (1000, 1000);
		}
	}

	// ゲームのスタート.
	void StartPlay () {
		cRocket.m_position.x = -4.0f;
		cRocket.m_position.y = 0.0f;
		cRocket.m_horizontalSpeed = -400.0f;
		cRocket.m_verticalSpeed = 0.0f;
		cRocket.m_rotate = 0.0f;
		cRocket.m_rocket.transform.Rotate (new Vector3 (0, 0, 1) * 90);
		cRocket.m_fuel = 1000;
		cRocket.m_downKeyLeft = false;
		cRocket.m_downKeyRight = false;
		cRocket.m_landing = false;
		cRocket.m_forcedLanding = false;
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
		if (cRocket.m_landing) {
			Transit (eStatus.eGameClear);
		}
		// 着地失敗.
		if (cRocket.m_forcedLanding) {
			Transit (eStatus.eGameOver);
		}
	}

	// ゲームクリアのスタート.
	void StartGameClear () {
		Debug.Log("Landing");
		Destroy (cRocket.m_propulsion);
	}

	// ゲームクリアのアップデート.
	void UpdateGameClear () {

	}

	// ゲームオーバーのスタート.
	void StartGameOver () {
		Debug.Log (Screen.width);
		cRocket.Explosion ();
		Destroy (cRocket.m_rocket);
		Destroy (cRocket.m_propulsion);
	}

	// ゲームオーバーのアップデート.
	void UpdateGameOver () {

	}
}
