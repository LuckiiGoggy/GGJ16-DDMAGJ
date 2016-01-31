using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    public int m_RequiredSacrifices;
    public int m_CurrentSacrificeCount;

	public Player m_Owner;


    public void FixedUpdate()
    {
		if (m_Owner != null) {
			if(m_CurrentSacrificeCount >= m_RequiredSacrifices)
			{
				Debug.Log("Enough Sacrifices");
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
        m_CurrentSacrificeCount += m_RequiredSacrifices;
		sacrifice.GetComponent<Sacrifice> ().Respawn ();
		Destroy (sacrifice);

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
