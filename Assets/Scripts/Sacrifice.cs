using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {
	
	private GameObject mOwner;

	public void Respawn() {
		Instantiate (gameObject);
	}

	public void SetOwner(GameObject owner) {
		mOwner = owner;
	}
}
