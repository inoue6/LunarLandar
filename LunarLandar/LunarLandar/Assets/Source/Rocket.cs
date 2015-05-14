using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	public GameObject m_rocket;			// ロケット.
	public GameObject m_propulsion;		// 推進装置の炎.
	public GameObject m_explosion;		// 爆発.
	public Vector2 m_position;			// 座標.
	public float m_horizontalSpeed;		// 水平.
	public float m_verticalSpeed;		// 垂直.
	public float m_rotate;				// 回転.
	public int m_fuel;					// 燃料.
	public bool m_propulsionFlag;		// 推進装置使用中.
	public bool m_landing;				// 着地成功.
	public bool m_forcedLanding;		// 着地失敗.

	// 後に押したキーを優先させる時に使う.
	public bool m_downKeyLeft;
	public bool m_downKeyRight;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	// 初期化.
	public void Initialize () {
		m_position.x = -4.0f;
		m_position.y = 0.0f;
		m_horizontalSpeed = -400.0f;
		m_verticalSpeed = 0.0f;
		m_rotate = 0.0f;
		m_rocket.transform.Rotate (new Vector3 (0, 0, 1) * 90);
		m_fuel = 1000;
		m_downKeyLeft = false;
		m_downKeyRight = false;
		m_landing = false;
		m_forcedLanding = false;

		m_rocket.transform.position = m_position;
	}

	// 推進装置.
	public void PropulsionSystem () {
		m_horizontalSpeed -= m_rocket.transform.up.x * 3;
		m_verticalSpeed -= m_rocket.transform.up.y * 3;
		// 燃料消費.
		if (m_fuel > 0) {
			m_fuel -= 1;
		}
		m_propulsionFlag = true;
	}

	// 回転.
	public void Rotate () {
		m_rocket.transform.Rotate (new Vector3 (0, 0, 1) * m_rotate);
		m_rotate = 0.0f;
	}

	// 垂直にする.
	public void Vertical () {
		m_rocket.transform.up = new Vector2 (0, 1);
	}

	// 着地成功条件チェック.
	public bool CheckClear () {
		if (((int)m_horizontalSpeed >= -100 && (int)m_horizontalSpeed <= 100) &&
			((int)m_verticalSpeed >= -100 && (int)m_verticalSpeed <= 100)) {
			return true;
		}
		return false;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		// 着地失敗かどうか.
		if (collider.CompareTag ("Moon")) {
			m_forcedLanding = true;
		}
	}

	// ロケット更新.
	public void UpdateRocket () {
		// 回転更新
		Rotate ();

		// 座標更新.
		m_verticalSpeed += 1f;
		m_position.x -= Mathf.FloorToInt(m_horizontalSpeed) * 0.00003f;
		m_position.y -= Mathf.FloorToInt(m_verticalSpeed) * 0.00003f;
		m_rocket.transform.position = m_position;

		if (m_propulsionFlag) {
			// 推進装置の炎.
			m_propulsion.transform.position = m_rocket.transform.position;
			m_propulsion.transform.rotation = m_rocket.transform.rotation;
		}
	}

	// 操作関係.
	public void OperationRocket () {
		if (Input.GetKey (KeyCode.DownArrow)) {
			// 推進装置.
			PropulsionSystem ();
		}
		else {
			if(m_propulsionFlag) {
				// 推進装置の炎を消す.
				m_propulsion.transform.position = new Vector2(1000, 1000);
			}
			m_propulsionFlag = false;
		}
		
		// 左回転.
		if (Input.GetKeyDown (KeyCode.LeftArrow) || m_downKeyLeft) {
			m_rotate = 1.0f;
			m_downKeyLeft = true;
			m_downKeyRight = false;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			m_downKeyLeft = false;
		}
		
		// 右回転.
		if (Input.GetKeyDown (KeyCode.RightArrow) || m_downKeyRight) {
			m_rotate = -1.0f;
			m_downKeyLeft = false;
			m_downKeyRight = true;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			m_downKeyRight = false;
		}

		// 着地場所と垂直に.
		if(Input.GetKeyDown (KeyCode.UpArrow)) {
			Vertical ();
		}
	}
	
	// 爆発.
	public void Explosion () {
		Instantiate (m_explosion, m_rocket.transform.position, Quaternion.identity);
	}
}
