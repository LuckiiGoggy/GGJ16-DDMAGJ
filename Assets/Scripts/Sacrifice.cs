using UnityEngine;
using System.Collections;


public enum SacrificeType {
	Lamb = 0,
	Chicken = 1,
	Totem = 2
}

public class Sacrifice : MonoBehaviour {
	public SacrificeType m_SacrificeType;

	void OnCollisionEnter2D(Collision2D coll) {
		AudioSource[] sounds = GetComponentsInChildren<AudioSource> ();
		sounds [Random.Range(0, sounds.Length)].Play ();
	}
}
