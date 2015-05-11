using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
	public GameObject rocket;
	public GameObject propulsion;
	//public GameObject explosion;
	public Vector2 position;
	public float horizontalSpeed;		// 水平
	public float verticalSpeed;			// 垂直
	public float rotate;
	public int fuel;
	public bool propulsionFlag;

	// 後に押したキーを優先させる時に使う
	bool downKeyLeft;
	bool downKeyRight;

	// Use this for initialization
	void Start () {
		position.x = -4.0f;
		position.y = 0.0f;
		horizontalSpeed = -400.0f;
		verticalSpeed = 0.0f;
		rotate = 0.0f;
		rocket.transform.Rotate (new Vector3 (0, 0, 1) * 90);
		fuel = 1000;
		downKeyLeft = false;
		downKeyRight = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fuel > 0) {
			if (Input.GetKey (KeyCode.DownArrow)) {
				PropulsionSystem ();
			}
			else {
				if(propulsionFlag) {
					propulsion.transform.position = new Vector2(1000, 1000);
				}
				propulsionFlag = false;
			}

			if (Input.GetKeyDown (KeyCode.LeftArrow) || downKeyLeft) {
				rotate = 1.0f;
				downKeyLeft = true;
				downKeyRight = false;
			}
			if (Input.GetKeyUp (KeyCode.LeftArrow)) {
				downKeyLeft = false;
			}
			if (Input.GetKeyDown (KeyCode.RightArrow) || downKeyRight) {
				rotate = -1.0f;
				downKeyLeft = false;
				downKeyRight = true;
			}
			if (Input.GetKeyUp (KeyCode.RightArrow)) {
				downKeyRight = false;
			}
		}
		else {
			if(propulsionFlag) {
				propulsion.transform.position = new Vector2(1000, 1000);
			}
			propulsionFlag = false;
		}

		if(Input.GetKeyDown (KeyCode.UpArrow)) {
			Vertical ();
		}

		Rotate ();

		verticalSpeed += 1f;
		position.x -= horizontalSpeed * 0.00003f;
		position.y -= verticalSpeed * 0.00003f;
		rocket.transform.position = position;
		if (propulsionFlag) {
			propulsion.transform.position = rocket.transform.position;
			propulsion.transform.rotation = rocket.transform.rotation;
		}
	}

	// 推進装置
	void PropulsionSystem () {
		horizontalSpeed -= rocket.transform.up.x * 3;
		verticalSpeed -= rocket.transform.up.y * 3;
		if (fuel > 0) {
			fuel -= 1;
		}
		propulsionFlag = true;
	}

	// 回転
	void Rotate () {
		rocket.transform.Rotate (new Vector3 (0, 0, 1) * rotate);
		rotate = 0.0f;
	}

	// 垂直にする
	void Vertical () {
		rocket.transform.up = new Vector2 (0, 1);
	}

	// 着地失敗
	void OnTriggerEnter2D (Collider2D collider) {
		Destroy (rocket);
	}

	// 爆発
	/*void Explosion () {
		Instantiate (explosion, transform.position, transform.rotation);
	}*/
}
