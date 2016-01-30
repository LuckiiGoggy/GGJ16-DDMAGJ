using UnityEngine;
using System.Collections;

public class TopDownControl : MonoBehaviour {

    public KeyCode m_Up;
    public KeyCode m_Down;
    public KeyCode m_Left;
    public KeyCode m_Right;

    public float m_MovementSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(m_Up)) transform.Translate(Vector3.up * m_MovementSpeed);
        if (Input.GetKey(m_Down)) transform.Translate(Vector3.down * m_MovementSpeed);
        if (Input.GetKey(m_Left)) transform.Translate(Vector3.left * m_MovementSpeed);
        if (Input.GetKey(m_Right)) transform.Translate(Vector3.right * m_MovementSpeed);
	
	}

    public void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);
    }
}
