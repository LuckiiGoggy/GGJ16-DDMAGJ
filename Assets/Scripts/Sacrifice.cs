using UnityEngine;
using System.Collections;

public enum SacrificeType {
	Lamb = 0,
	Chicken = 1,
	Totem = 2
}

public class Sacrifice : MonoBehaviour {
	public GameObject m_Sacrifice;

	private GameObject m_Owner;
	private SacrificeType m_SacrificeType;
	public SacrificeType SacrificeType { get { return m_SacrificeType; } }


	public void Respawn() {
        /*
		Instantiate(m_Sacrifice, Vector3.zero, Quaternion.identity);
        int randomNumber = Random.Range(0, 2);
        switch (randomNumber) {
            case 0:
				m_SacrificeType = SacrificeType.Lamb;
                break;
            case 1:
				m_SacrificeType = SacrificeType.Chicken;
                break;
            case 2:
				m_SacrificeType = SacrificeType.Totem;
                break;
			default:
				m_SacrificeType = SacrificeType.Lamb;
                break;
        }*/
	}

	public void SetOwner(GameObject owner) {
		m_Owner = owner;
	}
}
