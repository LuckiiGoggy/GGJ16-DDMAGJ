using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Spawner spawns sacrificial items as well as powers randomly
/// </summary>
public class Spawner : MonoBehaviour {


	public List<Transform> m_SacrificalItems;
	public List<Transform> m_PowerUps;

	// 0-1 float value for percentage of chance to spawn a sacrifice
	public float m_ChanceForSacrifice;

	// Use this for initialization
	void Start () {
		Instantiate(m_SacrificalItems[Random.Range(0, m_SacrificalItems.Count)]);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void Spawn()
    {
        print("Spawning ");
		if (Random.value >= m_ChanceForSacrifice)
		{
            print("Spawning Sacrifice");
			Instantiate(m_SacrificalItems[Random.Range(0, m_SacrificalItems.Count)]);
		}
		else
		{
            print("Spawning PowerUp");
			Instantiate(m_PowerUps[Random.Range(0, m_PowerUps.Count)]);
		}
	}


}
