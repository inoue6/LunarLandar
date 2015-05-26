using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	const float Width = 13.35f;

	public enum BackType {
		e_backGround1,
		e_backGround2,
	}

	public Rocket m_rocket;
	public BackType m_back;
	public Vector2 m_backPos1;
	public Vector2 m_backPos2;

	// Use this for initialization
	void Start () {
	}

	public void Initialize(){
		m_backPos1 = new Vector2 (m_rocket.m_position.x, m_rocket.m_position.y);
		m_backPos2 = new Vector2 (m_backPos1.x+Width,m_backPos1.y);
		SetPosition ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 位置をセットする関数
	public void SetPosition(){
		switch(m_back){
		case BackType.e_backGround1:
			transform.position = m_backPos1;
			break;
		case BackType.e_backGround2:
			transform.position = m_backPos2;
			break;
		}
	}

	// 交互に背景をセットする関数
	public void SetBackGround(){
		// 上下の移動はロケットに従う
		m_backPos1.y = m_rocket.m_position.y;
		m_backPos2.y = m_rocket.m_position.y;

		// ロケットが右へ移動している時 交互に背景をセットする
		if ((m_rocket.m_position.x - m_backPos1.x) >= Width) {
			m_backPos1.x = m_backPos2.x+Width;
		}
		if ((m_rocket.m_position.x - m_backPos1.x) >= 0.0f) {
			m_backPos2.x = m_backPos1.x+Width;
		}

		// ロケットが左へ移動している時 交互に背景をセットする
		if((m_rocket.m_position.x - m_backPos1.x) < 0.0f){
			// next right->left
			m_backPos2.x = m_backPos1.x-Width;
		}
		if ((m_rocket.m_position.x - m_backPos1.x) <= -Width) {
			m_backPos1.x=m_backPos2.x-Width;
		}

		SetPosition();
	}
}
