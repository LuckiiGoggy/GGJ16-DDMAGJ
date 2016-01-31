using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {

	public float m_MovementSpeed;
	private GameObject m_Sacrifice;

	public KeyCode m_DropSacrifice;
	public KeyCode m_Attack;

	private Sprite m_IdleSprite;
	private Sprite m_AttackSprite;


	private bool m_IsAttacking;
	private float m_AnimationTimer;
	public float m_AnimationLength;

	#region Player States

	public bool m_IsInvulnerable;
	public bool m_IsInGodMode;

	#endregion

	#region Animation Variables
	public float m_TransformationLength;
	#endregion



	#region Debuffs
	public enum Debuffs { Stun, Slow }

	public Dictionary<Debuffs, float> m_DebuffTimers;
    public float m_DebuffGracePeriod;
    public float m_DebuffGracePeriodTime;

	public KeyCode m_ForceSlow;
	#endregion

	#region Debug
	public List<Debuffs> m_DebuffTimersKeys;
	public List<float> m_DebuffTimersValues;
	public List<PowerStates> m_PowerStateTimersKeys;
	public List<float> m_PowerStateTimersValues;

	#endregion

	#region Power States

	private Animator animator;
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
                GetComponentInChildren<Shield>().GetComponent<SpriteRenderer>().enabled = true;
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
                control.enabled = false;
                m_DebuffGracePeriodTime = m_DebuffGracePeriod + duration;
				break;
			case Debuffs.Slow:
				control._fMoveSpeed = Mathf.Min(m_MovementSpeed * modifier, control._fMoveSpeed);
				break;
			default:
				break;
		}
	}

	/*
	public void PickUpSacrifice(GameObject sacrifice)
	{
		print("pick up sacrafice");
		sacrifice.SetActive (false);
		sacrifice.GetComponent<Sacrifice> ().SetOwner (gameObject);
		m_Sacrifice = sacrifice;
	}
	
	public void DropSacrifice(Vector3 away)
	{
		if (m_Sacrifice != null)
		{
			print("drop sacrafice");
			m_Sacrifice.SetActive (true);
			m_Sacrifice.transform.position = transform.position;
			m_Sacrifice.transform.Translate (away * 2);
			m_Sacrifice.GetComponent<Sacrifice> ().SetOwner (null);
			m_Sacrifice = null;
		}
	}

	public void PlaceSacrifice()
	{
		if (m_Sacrifice != null)
		{
			print("place sacrafice in box");
			m_Sacrifice.GetComponent<Sacrifice> ().SetOwner (null);
			m_Sacrifice = null;
		}
	}
	*/

	// Use this for initialization
	void Start () {
		m_IdleSprite = GetComponent<SpriteRenderer> ().sprite;
		m_AttackSprite = Resources.Load("battingv1", typeof(Sprite)) as Sprite;

		m_DebuffTimers = new Dictionary<Debuffs, float>();
		m_PowerStateTimers = new Dictionary<PowerStates, float>();

		m_DebuffTimers.Add(Debuffs.Slow, 0);
		m_DebuffTimers.Add(Debuffs.Stun, 0);

		m_PowerStateTimers.Add(PowerStates.Invulnerability, 0);
		m_PowerStateTimers.Add(PowerStates.SuperSpeed, 0);


		PlayerMovement control = gameObject.GetComponent<PlayerMovement>();
		control._fMoveSpeed = m_MovementSpeed;

		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GameObject.FindGameObjectsWithTag ("Player").Count () <= 1) {
			Destroy (gameObject.GetComponent<PlayerMovement> ());
			GameObject.Find ("EndGame").GetComponent<EndGame>().PlayerWins (GameObject.FindGameObjectWithTag ("Player"));
		}

		PlayerMovement control = gameObject.GetComponent<PlayerMovement>();

		foreach (Debuffs debuff in m_DebuffTimers.Keys.ToList()) 
			m_DebuffTimers[debuff] = Mathf.Max(m_DebuffTimers[debuff] - Time.fixedDeltaTime, 0);
		foreach (PowerStates powerState in m_PowerStateTimers.Keys.ToList()) 
			m_PowerStateTimers[powerState] = Mathf.Max(m_PowerStateTimers[powerState] - Time.fixedDeltaTime, 0);

        m_DebuffGracePeriodTime = Mathf.Max(m_DebuffGracePeriodTime - Time.fixedDeltaTime, 0);

		//Debug.Log("Before: " + control._fMoveSpeed);
		if(m_PowerStateTimers[PowerStates.SuperSpeed] == 0 &&
			m_DebuffTimers[Debuffs.Slow] == 0 )
		{
			control._fMoveSpeed = m_MovementSpeed;
		}

        if (m_DebuffTimers[Debuffs.Stun] == 0) control.enabled = true;



		//Debug.Log("After: " + control._fMoveSpeed);

        if (m_PowerStateTimers[PowerStates.Invulnerability] == 0)
        {
            m_IsInvulnerable = false;
            GetComponentInChildren<Shield>().GetComponent<SpriteRenderer>().enabled = false;
            
        }

		if (m_IsAttacking)
		{
			m_AnimationTimer += Time.fixedDeltaTime;

			if(m_AnimationTimer >= m_AnimationLength)
			{
				m_IsAttacking = false;
				GetComponent<SpriteRenderer> ().sprite = m_IdleSprite;
			}
		}

		#region Debug Updates
		m_DebuffTimersKeys = m_DebuffTimers.Keys.ToList();
		m_DebuffTimersValues = m_DebuffTimers.Values.ToList();

		m_PowerStateTimersKeys = m_PowerStateTimers.Keys.ToList();
		m_PowerStateTimersValues = m_PowerStateTimers.Values.ToList();

		#endregion

		CheckStates();

		HandleKeys();
	}

	private void CheckStates()
	{
		if (m_IsInGodMode) {
			GetComponentInChildren<Weapon> ().SetGodWeapon ();
		} else 
		{
			GetComponentInChildren<Weapon> ().ReverseGodWeapon ();
		}
	}

	void HandleKeys()
	{
		if (Input.GetKeyDown (m_Attack)) {
			GetComponentInChildren<Weapon>().Attack();
		}
		#region Debug Keys
		if (Input.GetKeyDown(m_ForceSlow)) ApplyDebuff(Debuffs.Slow, 0.5f, 5);
		/*if (Input.GetKeyDown(m_DropSacrifice)) {
			DropSacrifice(new Vector3(10, 10, 0));
		}*/
		#endregion
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
		var weapon = coll.gameObject.GetComponent<Weapon>();

		if(weapon != null && weapon != GetComponentInChildren<Weapon>())
		{
			if (weapon.m_IsAttacking)
			{
				print (string.Format("weapon {0} hit player {1}", coll.name, name));
				// if (m_Sacrifice != null) {

					ApplyDebuff (Debuffs.Slow, .1f, 10);
					// DropSacrifice (transform.position - coll.transform.position);
				//} else {
				//	m_SlowTimer = weapon.m_SlowLength;
				//}
				//if(m_DebuffTimers[Debuffs.Stun] > 0 || m_DebuffGracePeriodTime > 0) //Prevents Stun-locking
				//	ApplyDebuff(Debuffs.Stun, 42f, weapon.m_StunLength);


				if (weapon.IsGodWeapon () && !m_IsInvulnerable) {
					Destroy (this.gameObject); 
				}
            }
        }
    }


    public void GodModeOn()
    {
        if (m_IsInGodMode) return;

        m_IsInGodMode = true;

		animator.SetTrigger ("IsGod");

		ApplyDebuff(Debuffs.Slow, 0.5f, float.MaxValue);

        //Change Sprite
        //Activate Animation
        ApplyDebuff(Debuffs.Stun, 42f, m_TransformationLength);
		GameObject.Find ("Spawner").GetComponent<Spawner> ().Pause(m_TransformationLength);

    }
    public void GodModeOff()
    {
        if (!m_IsInGodMode) return;
        m_IsInGodMode = false;

		animator.SetTrigger ("Revert");

		ApplyDebuff(Debuffs.Slow, 1, 0);

        //Change Sprite
        //Activate Animation
        ApplyDebuff(Debuffs.Stun, 42f, m_TransformationLength);
    }
}
