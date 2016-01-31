using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

    public float m_TimeAlive;

	// Use this for initialization
	void Start () {
	    Destroy(this.gameObject, m_TimeAlive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
