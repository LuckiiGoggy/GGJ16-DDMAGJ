using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("Player collides Sacrifice");
			coll.gameObject.SendMessage ("PickUpSacrafice");
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
