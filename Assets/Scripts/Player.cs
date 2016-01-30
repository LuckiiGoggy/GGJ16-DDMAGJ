using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public float m_StunTimer;
    public float m_SlowTimer;

    public float m_MovementSpeed;
    public GameObject sacrifice;

    private bool mIsHoldingSacrafice;

    #region Player States

    private bool m_IsInvulnerable;
    private bool m_IsInGodMode;

    #endregion

    #region Debuffs
    public enum Debuffs { Stun, Slow }

    public Dictionary<Debuffs, float> m_DebuffTimers;


    #endregion



    #region Power States

    /// <summary>
    /// The different power state variables the player has
    /// </summary>
    public enum PowerStates { SuperSpeed, Invulnerability };

    public Dictionary<PowerStates, float> m_PowerStateTimers;

    #endregion


    public void ApplyPowerUp(PowerStates powerState, float modifier, float duration)
    {
        PlayerMovement control = GetComponent<PlayerMovement>();
        m_PowerStateTimers[powerState] = duration;

        switch (powerState)
        {
            case PowerStates.SuperSpeed:
                control._fMoveSpeed = Mathf.Max(control._fMoveSpeed, m_MovementSpeed * modifier);
                break;
            case PowerStates.Invulnerability:
                m_IsInvulnerable = true;
                break;
            default:
                break;
        }
    }

    public void ApplyDebuff(Debuffs debuff, float modifier, float duration)
    {
        PlayerMovement control = GetComponent<PlayerMovement>();
        m_DebuffTimers[debuff] = duration;

        switch (debuff)
        {
            case Debuffs.Stun:
                control._fMoveSpeed = Mathf.Min(m_MovementSpeed * 0.5f, 0);
                m_SlowTimer = Mathf.Max(m_SlowTimer - Time.fixedDeltaTime, 0);
                break;
            case Debuffs.Slow:
                control._fMoveSpeed = Mathf.Min(m_MovementSpeed * modifier, control._fMoveSpeed);
                m_SlowTimer = Mathf.Max(m_SlowTimer - Time.fixedDeltaTime, 0);
                break;
            default:
                break;
        }
    }


	public void PickUpSacrifice(GameObject sacrafice)
    {
		print("pick up sacrafice");
		Destroy (sacrafice);
        mIsHoldingSacrafice = true;
    }

    public void DropSacrifice()
    {
        if (mIsHoldingSacrafice)
        {
            print("drop sacrafice");
            mIsHoldingSacrafice = false;

            Instantiate(sacrifice);
            // Instantiate (sacrifice, transform.position, Quaternion.identity);
        }
    }

    public void PlaceSacrifice()
    {
        if (mIsHoldingSacrafice)
        {
            print("place sacrafice in box");
			mIsHoldingSacrafice = false;

			Instantiate (sacrifice);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        PlayerMovement control = gameObject.GetComponent<PlayerMovement>();

        foreach (Debuffs debuff in m_DebuffTimers.Keys) 
            m_DebuffTimers[debuff] = Mathf.Max(m_DebuffTimers[debuff] - Time.fixedDeltaTime, 0);
        foreach (PowerStates powerState in m_PowerStateTimers.Keys) 
            m_PowerStateTimers[powerState] = Mathf.Max(m_PowerStateTimers[powerState] - Time.fixedDeltaTime, 0);
        

        if(m_PowerStateTimers[PowerStates.SuperSpeed] == 0 &&
            m_DebuffTimers[Debuffs.Slow] == 0 &&
            m_DebuffTimers[Debuffs.Stun] == 0)
        {
            control._fMoveSpeed = m_MovementSpeed;
        }

        if (m_PowerStateTimers[PowerStates.Invulnerability] == 0) m_IsInvulnerable = false;
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

	void OnCollisionEnter2D(Collision2D coll)
	{
		print ("player collides with something");
		if (coll.gameObject.tag == "Sacrifice") {
			print ("Player collides Sacrifice");
			PickUpSacrifice (coll.gameObject);
		}
	}

    public void GodModeOn() { m_IsInGodMode = true; }
    public void GodModeOff() { m_IsInGodMode = false; }
}
