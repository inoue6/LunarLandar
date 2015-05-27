using UnityEngine;
using System.Collections;

using System.Linq;
using System;

public class Rocket : MonoBehaviour {
	const int positionNum = 8;
	const int MaxFuel = 1000;			// 燃料の最大値

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
	public int m_stageReachingNum;		// ステージ到達数.
	public int m_rotationAngle;			// 回転角度
	public float m_time;
	public int m_color;
	public int m_green;
	public int m_blue;
	
	int m_nowStageNum;							// 現在のゲーム開始位置の番号
	int[] m_StageNum = new int[positionNum];	// 開始位置をシャッフルして持つ配列
	int m_subStageNum;							// m_StageNumの添え字

	public Vector2[] m_setPosition = new Vector2[positionNum];
	// 後に押したキーを優先させる時に使う.
	public bool m_downKeyLeft;
	public bool m_downKeyRight;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < positionNum; i++) {
			m_setPosition[i] = new Vector2 (-14 + i*4, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 初期化.
	public void Initialize () {
		m_subStageNum = 0;
		ShuffleNum ();
		m_nowStageNum = m_StageNum [m_subStageNum];
		m_subStageNum++;
		m_position.x = m_setPosition[m_nowStageNum].x;
		m_position.y = 0.0f;
		m_horizontalSpeed = -400.0f;
		m_verticalSpeed = 0.0f;
		m_rotate = 0.0f;
		m_rocket.transform.Rotate (new Vector3 (0, 0, 1) * 90);
		m_fuel = MaxFuel;
		m_downKeyLeft = false;
		m_downKeyRight = false;
		m_landing = false;
		m_forcedLanding = false;

		m_rotationAngle = 90;

		m_rocket.transform.position = m_position;

		m_stageReachingNum++;

		m_time = 0;
		m_color = 255;
		m_green = 255;
		m_blue = 255;
	}

	// ステージ移動時の初期化.
	public void NextStageInitialize () {
		transform.up = new Vector2 (-1, 0);
		SetStartPosNum ();
		m_position.x = m_setPosition[m_nowStageNum].x;
		m_position.y = 0.0f;
		m_horizontalSpeed = -400.0f;
		m_verticalSpeed = 0.0f;
		m_rotate = 0.0f;

		m_fuel += 400;
		// 燃料の補給時に最大値を超えないようにする
		if (m_fuel > MaxFuel) {
			m_fuel = MaxFuel;
		}

		m_downKeyLeft = false;
		m_downKeyRight = false;
		m_landing = false;
		m_forcedLanding = false;

		m_rotationAngle = 90;

		m_rocket.transform.position = m_position;

		m_stageReachingNum++;

		m_time = 0;
		m_color = 255;
		m_green = 255;
		m_blue = 255;
	}

	// ロケットの開始位置の番号をシャッフルして配列に格納しておく関数
	public void ShuffleNum(){
		// 指定した数値から指定の数分だけ配列を生成し その後中身をシャッフルする
		// よりバラバラにするために前半と後半に分けてシャッフルをする
		int[] data1 = Enumerable.Range (0, positionNum/2).ToArray ().OrderBy (i => Guid.NewGuid ()).ToArray ();
		int[] data2 = Enumerable.Range (4, positionNum/2).ToArray ().OrderBy (i => Guid.NewGuid ()).ToArray ();

		// 結合する(交互に格納)
		for (int i=0; i<positionNum; i++) {
			if(i%2==0){
				m_StageNum[i]=data1[i/2];
			}
			else{
				m_StageNum[i]=data2[i/2];
			}
		}
	}

	// 次の開始位置の番号を設定する関数
	public void SetStartPosNum(){
		// 次の開始位置が今の開始位置に近すぎる場合 次の値に変更する
		while(m_subStageNum < positionNum &&
		      System.Math.Abs (m_nowStageNum - m_StageNum [m_subStageNum]) <= 1){
			m_subStageNum++;
		}
		
		// 配列の中身をすべて見終わった時 シャッフルする
		if (m_subStageNum >= positionNum) {
			m_subStageNum=0;
			ShuffleNum();
		}

		m_nowStageNum = m_StageNum [m_subStageNum];
		m_subStageNum++;
	}

	// 推進装置.
	public void PropulsionSystem () {
		m_horizontalSpeed -= m_rocket.transform.up.x * 3;
		m_verticalSpeed -= m_rocket.transform.up.y * 6;
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

		// 回転角度がロケットが垂直であるのを基準として一回転した時、角度が超えないようにする
		if (m_rotationAngle >= 360 || m_rotationAngle <= -360) {
			m_rotationAngle=0;
		}
	}

	// 垂直にする.
	public void Vertical () {
		m_rocket.transform.up = new Vector2 (0, 1);
	}

	// 着地成功条件チェック
	public bool CheckClear () {
		// m_verticalSpeedは表示時に符号を反転させて表示されているので
		// テキストで確認する範囲判定のために反転しなおす
		int verticalSpeed = -(int)m_verticalSpeed;

		if (((int)m_horizontalSpeed >= -100 && (int)m_horizontalSpeed <= 100) &&
		    (verticalSpeed >= -100 && verticalSpeed <= 0) && 
		    CheckRotationAngle()) {
			m_forcedLanding = false;
			return true;
		}
		return false;
	}

	// ロケットの傾きが着陸可能な範囲内かチェックする関数
	public bool CheckRotationAngle(){
		if(m_rotationAngle >= 3 || m_rotationAngle <= -3){
			return false;
		}
		return true;
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
		m_verticalSpeed += 2f;
		m_position.x -= Mathf.FloorToInt(m_horizontalSpeed) * 0.00006f;
		m_position.y -= Mathf.FloorToInt(m_verticalSpeed) * 0.00006f;
		m_rocket.transform.position = m_position;

		if (m_propulsionFlag) {
			// 推進装置の炎.
			m_propulsion.transform.position = m_rocket.transform.position;
			m_propulsion.transform.rotation = m_rocket.transform.rotation;
		}

		RocketColorChange ();
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
			m_rotationAngle+=1;
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			m_downKeyLeft = false;
		}
		
		// 右回転.
		if (Input.GetKeyDown (KeyCode.RightArrow) || m_downKeyRight) {
			m_rotate = -1.0f;
			m_downKeyLeft = false;
			m_downKeyRight = true;
			m_rotationAngle -=1;
		}
		if (Input.GetKeyUp (KeyCode.RightArrow)) {
			m_downKeyRight = false;
		}

		// 着地場所と垂直に.
		if(Input.GetKeyDown (KeyCode.UpArrow)) {
			Vertical ();
			m_rotationAngle = 0;
		}
	}
	
	// 爆発.
	public void Explosion () {
		Instantiate (m_explosion, m_rocket.transform.position, Quaternion.identity);
	}

	public void RocketColorChange () {
		m_time += Time.deltaTime;
		SpriteRenderer spriteRenderer = m_rocket.GetComponent<SpriteRenderer> ();
		if (m_fuel <= 300) {
			if(m_fuel <= 0) {
				spriteRenderer.color = Color.red;
				return;
			}
			if(m_time >= 0.5f) {
				m_color *= -1;
				spriteRenderer.color = new Color (255, m_green + m_color, m_blue + m_color, 255);
				m_time = 0;
			}
		}
		else {
			spriteRenderer.color = Color.white;
		}
	}
}
