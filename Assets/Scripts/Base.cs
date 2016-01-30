using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    public int m_RequiredSacrifices;
    private int m_CurrentSacrificeCount;

    public GameObject playerObject;
	private Player player;


    public void Start()
    {
		player = playerObject.GetComponent<Player>();
    }

    public void FixedUpdate()
    {
        if(m_CurrentSacrificeCount >= m_RequiredSacrifices)
        {
            player.GodModeOn();
        }
        else
        {
            player.GodModeOff();
        }
    }

    public void AddSacrifice()
    {
        m_CurrentSacrificeCount += m_RequiredSacrifices;
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			print ("Player triggers base");
			Player player = coll.gameObject.GetComponent<Player> ();
			player.PlaceSacrifice();
		}
	}
}
