using UnityEngine;
using System.Collections;

public class TopDownControl : MonoBehaviour {
	
    public KeyCode m_Up;
    public KeyCode m_Down;
    public KeyCode m_Left;
    public KeyCode m_Right;
	public KeyCode m_Drop;

    public KeyCode m_Attack;

    public float m_MovementSpeed;

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKey(m_Up)) Move(Vector3.up * m_MovementSpeed);
        if (Input.GetKey(m_Down)) Move(Vector3.down * m_MovementSpeed);
        if (Input.GetKey(m_Left)) Move(Vector3.left * m_MovementSpeed);
        if (Input.GetKey(m_Right)) Move(Vector3.right * m_MovementSpeed);
        if (Input.GetKeyDown(m_Attack)) GetComponentInChildren<Weapon>().Attack();
	}

    void Move(Vector3 moveVec)
    {
        transform.Translate(moveVec, Space.World);
        var angle = Vector3.Angle(Vector3.up, moveVec);
        angle = moveVec.x < 0.0f ? angle : -angle;


        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
