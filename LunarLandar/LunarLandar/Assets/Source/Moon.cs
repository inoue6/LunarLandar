using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour {
	public CameraControl m_camera;

	// Use this for initialization
	void Start () {
		m_camera.collisionCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// コライダー衝突.
	void OnTriggerEnter2D (Collider2D collider) {
		// カメラ拡大.
		if (collider.gameObject.tag == "MainCamera") {
			m_camera.collisionCount++;
			m_camera.ZoomInCamera ();
		}
	}

	// コライダー離れた
	void OnTriggerExit2D (Collider2D collider) {
		// カメラ縮小.
		if (collider.gameObject.tag == "MainCamera") {
			m_camera.collisionCount--;
			if(m_camera.collisionCount == 0) {
				m_camera.ZoomOutCamera ();
			}
		}
	}
}
