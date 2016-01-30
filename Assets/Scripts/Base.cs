using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    public int m_RequiredSacrifices;
    private int m_CurrentSacrificeCount;

	public GameObject Player;

    public void Start()
    {
		
    }

    public void FixedUpdate()
    {
        if(m_CurrentSacrificeCount >= m_RequiredSacrifices)
        {
			Player.GetComponent<Player>().GodModeOn();
        }
        else
        {
			Player.GetComponent<Player>().GodModeOff();
        }
    }

    public void AddSacrifice()
    {
        m_CurrentSacrificeCount += m_RequiredSacrifices;
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		print ("base triggers with something: " + coll.gameObject.tag);
		if (coll.gameObject == Player) {
			print ("Player triggers base");
			Player.GetComponent<Player> ().PlaceSacrifice();
			AddSacrifice ();
		}
	}
}
