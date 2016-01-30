using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    public int m_RequiredSacrifices;
    private int m_CurrentSacrificeCount;

	public string m_OwnerName;
	private GameObject m_Owner;

    public void Start()
    {
		m_Owner = GameObject.Find (m_OwnerName);
    }

    public void FixedUpdate()
    {
        if(m_CurrentSacrificeCount >= m_RequiredSacrifices)
        {
			m_Owner.GetComponent<Player>().GodModeOn();
        }
        else
        {
			//m_Owner.GetComponent<Player>().GodModeOff();
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
