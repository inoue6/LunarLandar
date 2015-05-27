using UnityEngine;
using System.Collections;

public class FuelMeter : MonoBehaviour {
	public GameObject m_gauge;
	public GameObject m_meter;
	public Rocket m_rocket;
	public Camera m_camera;
	public CameraControl m_cameraControl;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	// ワールド座標、大きさ決め.
	public void SetPosition () {
		m_meter.transform.position = new Vector3 (m_camera.transform.position.x + 6.24f, m_camera.transform.position.y - 2.19f, 0);
		
		Vector3 scale;
		Vector3 position;
		
		scale.x = 1;
		scale.y = m_rocket.m_fuel / 1000.0f;
		scale.z = 1;
		m_gauge.transform.localScale = new Vector3 (scale.x, scale.y, scale.z);
		m_meter.transform.localScale = new Vector3 (1, 1, 1);
		
		if (m_cameraControl.m_zoomIn) {
			position.x = m_camera.transform.position.x + 4.0f;
			position.y = m_camera.transform.position.y - 1.0f;
			m_meter.transform.position = new Vector3 (position.x, position.y, 0);
		}
		
		position.x = m_meter.transform.position.x;
		position.y = m_meter.transform.position.y - (float)((363 - (363 * scale.y)) / 200) - 0.25f;
		position.z = m_meter.transform.position.z;
		m_gauge.transform.position = new Vector3 (position.x, position.y, position.z);
	}

	// 残燃料によりメーターの色を変更.
	public void MeterColorChange () {
		Renderer renderer = m_gauge.GetComponent<Renderer> ();
		if (m_rocket.m_fuel > 500) {
			renderer.material.color = Color.green;
		}
		else if (m_rocket.m_fuel > 300) {
			renderer.material.color = Color.yellow;
		}
		else {
			renderer.material.color = Color.red;
		}
	}
}
