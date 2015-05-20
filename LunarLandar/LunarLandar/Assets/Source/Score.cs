using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	const int BaseHighScore = 100;		// 経過時間が残りの燃料よりも大きい時のクリアの基礎点
	const int BaseLowScore = 10;		// 経過時間が残りの燃料よりも小さい時のクリアの基礎点

	public Rocket m_Rocket;

	public float timer;
	public int score;
	//public int fuelRemaining;

	// Use this for initialization
	void Start () {
	}

	public void Initialize(){
		timer = 0.0f;
		score = 0;
		//fuelRemaining = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 1ステージのプレイ時間を取得する関数
	public void SetTime(float time){
		timer = time;
	}

	// スコアを計算する関数
	public void ComputeScore(){
		// 残りの燃料と経過時間の差でクリアの基礎点数を変える
		if ((m_Rocket.m_fuel - timer) <= 0) {
			// クリアの基礎点数+かかった時間と残りの燃料でスコアを計算
			score += (BaseLowScore + (int)((m_Rocket.m_fuel*10)/(int)timer));
		}
		else {
			score += (BaseHighScore + (int)((m_Rocket.m_fuel*10)/(int)timer));
		}
	}
}
