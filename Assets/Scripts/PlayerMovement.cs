using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private float _fCurrentMoveSpeed = 1;
	public float _fMoveSpeed = 1;

	public float _fSlowPlayerTimer = 3.0f;
	public float _fStunPlayerTimer = 3.0f;

	// depending on what type of JoystickNumber you chose, the following 2 strings get changed
	// to have names relative to their controller number's name in InputManager
	public string _horizontalAxis;
	public string _verticalAxis;

	public Vector2 _movement;

	private Rigidbody2D _rigidbody;
	private PlayerAnimation playerAnimation;

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D> ();
		playerAnimation = GetComponent<PlayerAnimation> ();
	}

	void FixedUpdate ()
	{
		// animation stuff
		if (Input.GetAxisRaw (_horizontalAxis) != 0 || Input.GetAxisRaw (_verticalAxis) != 0) {
			playerAnimation.animator.SetBool ("Walk", true);
		} else {
			playerAnimation.animator.SetBool ("Walk", false);
		}

		if (_rigidbody.velocity.magnitude > 0) {
			playerAnimation.m_animationsSpeed = _rigidbody.velocity.magnitude / 5;
		} else {
			playerAnimation.m_animationsSpeed = 1;
		}

		// animation stuff end

		_movement = new Vector2 (Input.GetAxisRaw(_horizontalAxis), Input.GetAxisRaw(_verticalAxis));
		_movement.Normalize ();
		_rigidbody.velocity = _movement * _fMoveSpeed;
	}

	private void SlowPlayer ()
	{
		StartCoroutine (SlowPlayerTimer ());
	}

	IEnumerator SlowPlayerTimer ()
	{
		_fCurrentMoveSpeed /= 2;
		yield return new WaitForSeconds (_fSlowPlayerTimer);
		_fCurrentMoveSpeed *= 2;
	}

	private void StunPlayer ()
	{
		StartCoroutine (StunPlayerTimer ());
	}

	IEnumerator StunPlayerTimer ()
	{
		_fCurrentMoveSpeed = 0;
		yield return new WaitForSeconds (_fStunPlayerTimer);
		_fCurrentMoveSpeed = _fMoveSpeed;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.name == "slow-box") 
		{
			SlowPlayer ();
		}
		else if (other.gameObject.name == "stun-box") 
		{
			StunPlayer ();
		}
	}
}
