using UnityEngine;
using System.Collections;

public class Landing : MonoBehaviour {
	const int footNum = 2;	// 着陸時の足の最大本数.
	public Rocket m_Rocket;
	int m_count;				// 着陸時の足の本数.

	// Use this for initialization
	void Start () {
		m_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// コライダー衝突.
	void OnTriggerEnter2D (Collider2D collider) {
		// 着地失敗かどうか.
		if (collider.CompareTag ("Rocket") && !m_Rocket.m_landing) {
			m_Rocket.m_forcedLanding = true;
		}
		// 着陸成功かどうか.
		if (collider.CompareTag ("Landing")) {
			m_count++;

			if (m_count >= footNum) {
				m_Rocket.m_landing = true;
				m_Rocket.m_forcedLanding = false;
			}
		}
	}
}
