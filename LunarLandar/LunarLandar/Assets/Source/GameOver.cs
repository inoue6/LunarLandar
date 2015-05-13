using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	GameObject cGameoverText;

	// Use this for initialization
	void Start () {
		cGameoverText = GameObject.Find ("GameOverText");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 爆発終了後の処理.
	void AnimationFinish () {
		Destroy (gameObject);
		cGameoverText.transform.position = new Vector2 (0.28f, 0.6f);
	}
}
