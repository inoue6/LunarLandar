using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public Camera m_camera;
	public Rocket m_rocket;
	public Vector3 m_position;
	public GameObject m_backGround;

	// Use this for initialization
	void Start () {
		m_position = new Vector3 (0, 0, -8.68f);
		transform.position = m_position;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera ();
		transform.position = m_position;
		m_backGround.transform.position = m_rocket.m_position;
	}

	// 移動.
	public void MoveCamera () {
		m_position.x = m_rocket.m_position.x;
		m_position.y = m_rocket.m_position.y;
	}

	// 拡大.
	public void ZoomInCamera () {
		m_position.z = -6;
	}

	// 縮小.
	public void ZoomOutCamera () {
		m_position.z = -8.68f;
	}
}
