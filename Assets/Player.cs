using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public GameObject sacrifice;

	private bool mIsHoldingSacrafice;

	public void PickUpSacrafice() {
		print ("pick up sacrafice");
		mIsHoldingSacrafice = true;
	}

	public void DropSacrafice() {
		if (mIsHoldingSacrafice) {
			print ("drop sacrafice");
			mIsHoldingSacrafice = false;

			Instantiate (sacrifice);
			// Instantiate (sacrifice, transform.position, Quaternion.identity);
		}
	}

	public void PlaceSacrafice() {
		if (mIsHoldingSacrafice) {
			print ("place sacrafice in box");
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
