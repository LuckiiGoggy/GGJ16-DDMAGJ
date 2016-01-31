using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour {
	
	public Player m_Owner;
	public float m_BaseInvulnerableLength;

	public bool SacrificesCompleted { 
		get { 
			return m_Completed[0] && m_Completed[1] && m_Completed[2];
		}
	}
			

    public Spawner m_Spawner;
	private bool[] m_Completed;


	void Start()
	{
		m_Completed = new bool[3];
	}


    public void FixedUpdate()
    {
        if (m_Owner != null)
        {
            if (SacrificesCompleted)
            {
                m_Owner.GetComponent<Player>().GodModeOn();
            }
            else
            {
                m_Owner.GetComponent<Player>().GodModeOff();
            }
        }
	}

	public void AddSacrifice(GameObject sacrifice)
	{
		m_Completed[(int) sacrifice.GetComponent<Sacrifice>().m_SacrificeType] = true;
		Destroy (sacrifice);

		if (!SacrificesCompleted) {
			m_Spawner.Spawn();
		}
	}

	public void RemoveSacrifice(GameObject sacrifice)
	{
		m_Completed[(int) sacrifice.GetComponent<Sacrifice>().m_SacrificeType] = false;
		Destroy (sacrifice);
		m_Spawner.Spawn();
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.gameObject.tag == "Item")
            m_Spawner.Spawn();

		else if (coll.gameObject.tag == "Sacrifice") {
			if (!SacrificesCompleted)
				AddSacrifice (coll.gameObject);
			else
				RemoveSacrifice (coll.gameObject);
		}

	}
}
