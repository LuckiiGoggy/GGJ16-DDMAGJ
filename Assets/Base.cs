using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Player") {
			print ("Player triggers base");
			Player player = coll.gameObject.GetComponent<Player> ();
			player.PlaceSacrifice();
		}
	}
}
