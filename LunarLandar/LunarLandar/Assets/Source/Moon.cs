﻿using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {
	const int footNum = 2;	// 着陸時の足の最大本数.
	public Rocket m_Rocket;
	public CameraControl m_camera;
	int count;				// 着陸時の足の本数.

	// Use this for initialization
	void Start () {
		count = 0;
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
			count++;

			if (count >= footNum) {
				m_Rocket.m_landing = true;
				m_Rocket.m_forcedLanding = false;
			}
		}
		
		// カメラ拡大.
		if (collider.gameObject.tag == "MainCamera") {
			m_camera.ZoomInCamera ();
		}
	}

	// コライダー離れた
	void OnTriggerExit2D (Collider2D collider) {
		// カメラ縮小.
		if (collider.gameObject.tag == "MainCamera") {
			m_camera.ZoomOutCamera ();
		}
	}
}
