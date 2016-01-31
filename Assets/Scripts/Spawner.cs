using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Spawner spawns sacrificial items as well as powers randomly
/// </summary>
public class Spawner : MonoBehaviour {


	public List<Transform> m_SacrificalItems;
	public List<Transform> m_PowerUps;
	private float m_PausedTime = -1;

	// 0-1 float value for percentage of chance to spawn a sacrifice
	public float m_ChanceForSacrifice;

	// Use this for initialization
	void Start () {
		Instantiate(m_SacrificalItems[Random.Range(0, m_SacrificalItems.Count)]);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (m_PausedTime == 0) {
			Spawn ();
			m_PausedTime--;
		}

		if (m_PausedTime > 0) {
			m_PausedTime = Mathf.Max(m_PausedTime - Time.fixedDeltaTime, 0);
		}
	}

	public void Pause(float length) {
		m_PausedTime = length;
	}

	public void Spawn()
    {
        print("Spawning ");
		if (Random.value * 100 <= m_ChanceForSacrifice)
		{
			Instantiate(m_SacrificalItems[Random.Range(0, m_SacrificalItems.Count)]);
		}
		else
		{
			Instantiate(m_PowerUps[Random.Range(0, m_PowerUps.Count)]);
		}
	}


}
