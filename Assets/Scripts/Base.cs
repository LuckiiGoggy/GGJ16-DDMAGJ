using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour {
	
	public Player m_Owner;
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
		if(m_Owner != null && SacrificesCompleted)
        {
			m_Owner.GetComponent<Player>().GodModeOn();
		}
		else
		{
			m_Owner.GetComponent<Player>().GodModeOff();
		}
	}

	public void AddSacrifice(GameObject sacrifice)
	{
		m_Completed[(int) sacrifice.GetComponent<Sacrifice>().m_SacrificeType] = true;
		Destroy (sacrifice);


		int i = 0;
		foreach (bool completed in m_Completed) {
			if (completed) i++;
		}
		print(i + " sacrifices completed");

	}

	void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.gameObject.tag == "Sacrifice" || coll.gameObject.tag == "Item")
            m_Spawner.Spawn();


		if (coll.gameObject.tag == "Sacrifice") {
			AddSacrifice (coll.gameObject);
		}

	}
}
