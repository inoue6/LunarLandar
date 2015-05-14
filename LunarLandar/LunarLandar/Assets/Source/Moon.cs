﻿using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {
	public CameraControl m_camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// コライダー衝突.
	void OnTriggerEnter2D (Collider2D collider) {
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
