using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {

	public GameObject m_Sacrifice;
	private GameObject mOwner;

	public void Respawn() {
		Instantiate (m_Sacrifice);
	}

	public void SetOwner(GameObject owner) {
		mOwner = owner;
	}
}
