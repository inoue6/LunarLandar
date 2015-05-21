using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	const float Width = 40.0f;											// ステージの幅
	static readonly Vector2 InitialPos = new Vector2(9.65f,-5.75f);		// ロケットがあるステージの初期位置

	public enum StageType {
		e_moonNow,
		e_moonNext,
	}

	public Rocket m_rocket;
	public Vector2 m_nowMoonPos;			// ロケットがいるステージの座標
	public Vector2 m_nextMoonPos;			// ロケットがスクロールして次に入ると考えられるステージの座標
	public StageType m_moon;
	bool nowMoonRight;						// ロケットがいるステージの右側に次のステージがセットされているか
	bool nowMoonLeft;						// ロケットがいるステージの左側に次のステージがセットされているか

	// Use this for initialization
	void Start () {
	}

	// 初期化
	public void Initialize(){
		m_nowMoonPos = InitialPos;
		m_nextMoonPos = new Vector2 (m_nowMoonPos.x+Width,m_nowMoonPos.y);
		SetPosition ();
		nowMoonRight = true;
		nowMoonLeft = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 位置をセットする関数
	public void SetPosition(){
		switch(m_moon){
		case StageType.e_moonNow:
			transform.position = new Vector2 (m_nowMoonPos.x,InitialPos.y);
			break;
		case StageType.e_moonNext:
			transform.position = new Vector2 (m_nextMoonPos.x,InitialPos.y);
			break;
		}
	}

	// 現在いるステージとスクロールで次に入るステージをロケットの位置でセットする関数
	public void SetStage(){
		// ロケットが右に移動している時のステージセット
		// nowMoonからnextMoonへロケットが移動した時 位置を入れ替える
		if (m_rocket.m_position.x - m_nowMoonPos.x+InitialPos.x >= (Width/2) && nowMoonRight) {
			Vector2 pos = m_nowMoonPos;
			m_nowMoonPos = m_nextMoonPos;
			m_nextMoonPos = pos;
			SetPosition();

			nowMoonLeft = true;
			nowMoonRight = false;
		}
		// ロケットがnowMoonの半分をこえた時 nextMoonをnowMoonのロケットの進行方向側にセットする
		if (m_rocket.m_position.x - m_nowMoonPos.x+InitialPos.x >= 0 && nowMoonLeft) {
			m_nextMoonPos.x = m_nowMoonPos.x+Width;
			SetPosition();

			nowMoonLeft = false;
			nowMoonRight = true;
		}

		// ロケットが左に移動している時のステージセット
		// ロケットがnowMoonの半分をこえた時 nextMoonをnowMoonのロケットの進行方向側にセットする
		if (m_rocket.m_position.x - m_nowMoonPos.x+InitialPos.x <= 0 && nowMoonRight) {
			m_nextMoonPos.x = m_nowMoonPos.x-Width;
			SetPosition();
			
			nowMoonLeft = true;
			nowMoonRight = false;
		}
		// nowMoonからnextMoonへロケットが移動した時 位置を入れ替える
		if (m_rocket.m_position.x - m_nowMoonPos.x+InitialPos.x <= -(Width/2) && nowMoonLeft) {
			Vector2 pos = m_nowMoonPos;
			m_nowMoonPos = m_nextMoonPos;
			m_nextMoonPos = pos;
			SetPosition();

			nowMoonLeft = false;
			nowMoonRight = true;
		}
	}
}
