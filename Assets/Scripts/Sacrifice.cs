using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {

	public GameObject m_Sacrifice;
	private GameObject mOwner;

	public static void Respawn() {
		
	}

	public void SetOwner(GameObject owner) {
		mOwner = owner;
	}
}
