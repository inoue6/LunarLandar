using UnityEngine;
using System.Collections;

public class Landing : MonoBehaviour {
	const int footNum = 2;	// 着陸時の足の最大本数.
	public Rocket cRocket;
	int count;				// 着陸時の足の本数.

	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// コライダーの当たり判定.
	void OnTriggerEnter2D (Collider2D collider) {
		// 着地失敗かどうか.
		if (collider.CompareTag ("Rocket") && !cRocket.m_landing) {
			cRocket.m_forcedLanding = true;
		}
		// 着陸成功かどうか.
		if (collider.CompareTag ("Landing")) {
			count++;

			if(count >= footNum) {
				cRocket.m_landing = true;
				cRocket.m_forcedLanding = false;
			}
		}
	}
}
