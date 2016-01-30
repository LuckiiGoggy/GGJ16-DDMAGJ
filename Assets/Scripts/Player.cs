using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float m_StunTimer;
    public float m_SlowTimer;

    public float m_MovementSpeed;
    public GameObject sacrifice;

    private bool mIsHoldingSacrafice;
    private bool m_IsInGodMode;

    public void PickUpSacrafice()
    {
        print("pick up sacrafice");
        mIsHoldingSacrafice = true;
    }

    public void DropSacrafice()
    {
        if (mIsHoldingSacrafice)
        {
            print("drop sacrafice");
            mIsHoldingSacrafice = false;

            Instantiate(sacrifice);
            // Instantiate (sacrifice, transform.position, Quaternion.identity);
        }
    }

    public void PlaceSacrafice()
    {
        if (mIsHoldingSacrafice)
        {
            print("place sacrafice in box");
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerMovement control = GetComponent<PlayerMovement>();

        if (m_StunTimer > 0)
        {
            control._fMoveSpeed = 0;
            m_StunTimer = Mathf.Max(m_StunTimer - Time.fixedDeltaTime, 0);
        }

        if (m_SlowTimer > 0)
        {
            control._fMoveSpeed = Mathf.Min(m_MovementSpeed * 0.5f, control._fMoveSpeed);
            m_SlowTimer = Mathf.Max(m_SlowTimer - Time.fixedDeltaTime, 0);
        }

        if(m_StunTimer == 0 && m_SlowTimer == 0)
        {
            control._fMoveSpeed = m_MovementSpeed;
        }
	}

    /// <summary>
    /// When the Player Collides with a weapon, it should:
    /// - drop its sacrifice if it's carry it
    ///     - and get stunned
    /// - slowed if its not carrying a sacrifice
    /// </summary>
    /// <param name="coll">The object that collided with the Player</param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        var weapon = coll.transform.GetComponent<Weapon>();

        if(weapon != null && weapon != GetComponentInChildren<Weapon>())
        {
            if (weapon.m_IsAttacking)
            {
                m_SlowTimer = weapon.m_SlowLength;
            }
        }
    }

    public void GodModeOn() { m_IsInGodMode = true; }
    public void GodModeOff() { m_IsInGodMode = false; }
}
