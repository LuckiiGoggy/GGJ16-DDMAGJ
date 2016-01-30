using UnityEngine;
using System.Collections;

public class Sacrifice : MonoBehaviour {

	public GameObject m_Sacrifice;
	private GameObject mOwner;
    public string sacrificeType;


	public void Respawn() {
		Instantiate (m_Sacrifice);
        int randomNumber = Random.Range(0, 2);
        switch (randomNumber) {
            case 0:
                sacrificeType = "Lamb";
                break;
            case 1:
                sacrificeType = "Chicken";
                break;
            case 2:
                sacrificeType = "Totem";
                break;
            default:
                sacrificeType = "Lamb";
                break;
        }
	}

	public void SetOwner(GameObject owner) {
		mOwner = owner;
	}
}
