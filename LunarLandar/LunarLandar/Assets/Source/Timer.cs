using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	float m_time;	// 計測するタイマー.
	int m_minute;	// 分.
	int m_second;	// 秒.
	int m_miri;		// ミリ秒.

	public float time {
		get { return this.m_time; }
		set { this.m_time = value; }
	}

	public int minute {
		get {
			m_minute = (int)(m_time / 60);
			return this.m_minute;
		}
		set { this.m_minute = value; }
	}

	public int second {
		get {
			m_second = (int)(m_time % 60);
			return this.m_second;
		}
		set { this.m_second = value; }
	}

	public int miri {
		get {
			m_minute = (int)(m_time / 60);
			m_second = (int)(m_time % 60);
			m_miri = (int)((m_time - (m_minute + m_second)) * 100);
			return this.m_miri;
		}
		set { this.m_miri = value; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 時間を計測.
	public void Measurement () {
		m_time += Time.deltaTime;
	}
}
