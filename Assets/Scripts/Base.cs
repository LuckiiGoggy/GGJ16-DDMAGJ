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

	public GameObject bottomTotem_deactivated;
	public GameObject middleTotem_deactivated;
	public GameObject topTotem_deactivated;

	public GameObject bottomTotem_activated;
	public GameObject middleTotem_activated;
	public GameObject topTotem_activated;

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
                m_Spawner.StartSpawningShields();
            }
            else
            {
                m_Owner.GetComponent<Player>().GodModeOff();
                m_Spawner.StopSpawningShields();
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

		if (SacrificesCompleted) 
		{
			ResetTotems ();
		}
		else 
		{
			ActivateRespectiveTotem ();
		}
	}

	void ActivateRespectiveTotem ()
	{
		if (m_Completed [0] == true) 
		{
			bottomTotem_deactivated.SetActive (false);
			bottomTotem_activated.SetActive (true);
		}
		if (m_Completed [1] == true) 
		{
			middleTotem_deactivated.SetActive (false);
			middleTotem_activated.SetActive (true);
		}
		if (m_Completed [2] == true) 
		{
			topTotem_deactivated.SetActive (false);
			topTotem_activated.SetActive (true);
		}
	}

	void ResetTotems ()
	{
		bottomTotem_deactivated.SetActive (true);
		bottomTotem_activated.SetActive (false);
		middleTotem_deactivated.SetActive (true);
		middleTotem_activated.SetActive (false);
		topTotem_deactivated.SetActive (true);
		topTotem_activated.SetActive (false);

		m_Owner.GetComponent<Player>().GodModeOff();
		m_Owner.GetComponentInChildren<Weapon>().m_IsGodWeapon = false;
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
