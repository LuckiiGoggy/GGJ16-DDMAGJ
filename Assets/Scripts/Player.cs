using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {

	public float m_MovementSpeed;
    protected GameObject m_Sacrifice;
    protected PauseGame m_PauseGame;

	public KeyCode m_DropSacrifice;
	public KeyCode m_Attack;
	public KeyCode m_Pause;

	protected Sprite m_IdleSprite;
    protected Sprite m_AttackSprite;

    public List<AudioClip> m_DeathSounds;
    public AudioSource m_AudioSource;

	public float m_GameEndLength;
	private bool m_GameEnded;



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
		PlayerRotation rotationController = GetComponent<PlayerRotation>();
		m_DebuffTimers[debuff] = duration;

		switch (debuff)
		{
			case Debuffs.Stun:
				control.enabled = false;
				rotationController.enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
		m_PauseGame = GameObject.Find ("PauseGame").GetComponent<PauseGame> ();
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

	void Update() {
		if (Input.GetKeyDown (m_Pause)) {
			if (Time.timeScale != 0) {
				m_PauseGame.Pause ();
			} else {
				m_PauseGame.Unpause ();
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (m_GameEnded) {
			m_GameEndLength -= Time.fixedDeltaTime;
			if (m_GameEndLength <= 0) {
				if (Input.GetKeyDown (m_Attack)) {
					Application.LoadLevel ("StartScene");
				}
			}
			return;
		}

		if (GameObject.FindGameObjectsWithTag ("Player").Count () <= 1) {
			Destroy (gameObject.GetComponent<PlayerMovement> ());
			Destroy (gameObject.GetComponent<PlayerRotation> ());
			GameObject.Find ("EndGame").GetComponent<EndGame> ().PlayerWins (GameObject.FindGameObjectWithTag ("Player"));
			m_GameEnded = true;
		}

        UpdateDebuffsAndPowerUps();

		#region Debug Updates
		m_DebuffTimersKeys = m_DebuffTimers.Keys.ToList ();
		m_DebuffTimersValues = m_DebuffTimers.Values.ToList ();

		m_PowerStateTimersKeys = m_PowerStateTimers.Keys.ToList ();
		m_PowerStateTimersValues = m_PowerStateTimers.Values.ToList ();

		#endregion

		CheckStates ();
		HandleKeys();
	}

    protected void UpdateDebuffsAndPowerUps()
    {
        PlayerMovement control = gameObject.GetComponent<PlayerMovement>();
		PlayerRotation rotationController = GetComponent<PlayerRotation>();

        foreach (Debuffs debuff in m_DebuffTimers.Keys.ToList())
            m_DebuffTimers[debuff] = Mathf.Max(m_DebuffTimers[debuff] - Time.fixedDeltaTime, 0);
        foreach (PowerStates powerState in m_PowerStateTimers.Keys.ToList())
            m_PowerStateTimers[powerState] = Mathf.Max(m_PowerStateTimers[powerState] - Time.fixedDeltaTime, 0);

        m_DebuffGracePeriodTime = Mathf.Max(m_DebuffGracePeriodTime - Time.fixedDeltaTime, 0);

        //Debug.Log("Before: " + control._fMoveSpeed);
        if (m_PowerStateTimers[PowerStates.SuperSpeed] == 0 &&
           m_DebuffTimers[Debuffs.Slow] == 0)
        {
            if(control != null)
                control._fMoveSpeed = m_MovementSpeed;
        }

		if (m_DebuffTimers [Debuffs.Stun] == 0) 
		{
			if (control != null) {
				control.enabled = true;
				rotationController.enabled = true;
			}
		}
            



        //Debug.Log("After: " + control._fMoveSpeed);

        if (m_PowerStateTimers[PowerStates.Invulnerability] == 0)
        {
            m_IsInvulnerable = false;
            GetComponentInChildren<Shield>().GetComponent<SpriteRenderer>().enabled = false;

        }
    }

	private void CheckStates()
	{
		if (m_IsInGodMode)
		{
			GetComponentInChildren<Weapon>().SetGodWeapon();
        }
        else
        {
            GetComponentInChildren<Weapon>().ReverseGodWeapon();
        }

	}

	void HandleKeys()
	{
		if (Input.GetKeyDown (m_Attack)) {
			GetComponentInChildren<Weapon>().Attack();
		}
		#region Debug Keys
		//if (Input.GetKeyDown(m_ForceSlow)) );
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
				ApplyDebuff (Debuffs.Slow, 0.5f, GetComponentInChildren<Weapon> ().m_SlowLength);

				if (weapon.IsGodWeapon () && !m_IsInvulnerable) {
                    //Get the angle where the spurt would be away from the god
                    Vector3 fromEnemy = weapon.transform.position - transform.position;
                    var angle = Vector3.Angle(Vector3.up, fromEnemy);
                    angle = fromEnemy.x < 0.0f ? angle : -angle;

                    //rotate the body to face away from the god
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    //start the player death particle
                    GetComponentInChildren<PlayerDeath>().GetComponent<ParticleSystem>().Play();

                    //Prepare for destruction
                    Death();
				}
            }
        }

    }
    
    /// <summary>
    /// Should be called when the player needs to be removed for any reason.
    /// </summary>
    void Death()
    {
        Destroy(this.gameObject, 1f);
        GetComponent<PlayerMovement>().enabled = false;
        m_AudioSource.PlayOneShot(m_DeathSounds[Random.Range(0, m_DeathSounds.Count)]);
        
        //GetComponent<Player>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Meteor")
            Death();
    }

    public virtual void GodModeOn()
    {
        //If already in god mode, don't restart the transformation
        if (m_IsInGodMode) return;

        m_IsInGodMode = true;
        animator = GetComponent<Animator>();
        animator.SetTrigger("IsGod");
        //Play transformation particles
        GetComponentInChildren<ParticleSystem>().Play();

		ApplyDebuff(Debuffs.Slow, 0.5f, float.MaxValue);

        //Stun the player while transforming
        ApplyDebuff(Debuffs.Stun, 42f, m_TransformationLength);

        //Prevent spawner from spawning while someone is transforming
		GameObject.Find ("Spawner").GetComponent<Spawner> ().Pause(m_TransformationLength);

    }
    public virtual void GodModeOff()
    {
        //If not already in god mode, don't turn it off
        if (!m_IsInGodMode) return;

        m_IsInGodMode = false;
        animator = GetComponent<Animator>();
        animator.SetTrigger ("Revert");
        //Player transformation particle
        GetComponentInChildren<ParticleSystem>().Play();

		ApplyDebuff(Debuffs.Slow, 1, 0.1f);

        //Stun the player while transforming
        ApplyDebuff(Debuffs.Stun, 42f, m_TransformationLength);

        //Prevent spawner from spawning while someone is transforming
        GameObject.Find("Spawner").GetComponent<Spawner>().Pause(m_TransformationLength);
    }
}
