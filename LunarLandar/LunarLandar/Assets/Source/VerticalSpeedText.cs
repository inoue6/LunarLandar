using UnityEngine;
using System.Collections;

public class VerticalSpeedText : MonoBehaviour {
	public Rocket cRocket;
	public GUIText cText;
	Vector2 position;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		cRocket.m_verticalSpeed *= -1;
		cText.text = "VerticalSpeed" + cRocket.m_verticalSpeed.ToString ();
		cRocket.m_verticalSpeed *= -1;
	}
}
