using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Base : MonoBehaviour {
	
	public Player m_Owner;
	public bool SacrificesCompleted { 
		get { 
			bool result = true;
			foreach (bool completed in m_Completed) {
				result = completed;
			}
			return result;
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
		m_Spawner.Spawn();

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
		print ("base triggers with something: " + coll.gameObject.tag);

		if (coll.gameObject.tag == "Sacrifice") {
			print ("Sacrifice triggers base");
			AddSacrifice (coll.gameObject);
		}

	}
}
