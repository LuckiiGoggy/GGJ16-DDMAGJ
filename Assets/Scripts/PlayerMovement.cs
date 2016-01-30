using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// these enum types have been configured in the InputManager
	public enum JoystickNumber { keyboard, one, two, three, four };
	public JoystickNumber joyStickNumber;

	public Vector2 _velocity;
	public float _magnitude;
	public float _fMoveSpeed = 1;

	// this is for testing to make sure we are getting input from controller
	public float joystickFloat;

	// depending on what type of JoystickNumber you chose, the following 2 strings get changed
	// to have names relative to their controller number's name in InputManager
	public string _horizontalAxis;
	public string _verticalAxis;

	private Rigidbody2D _rigidbody;

	void Awake ()
	{
		if (joyStickNumber == JoystickNumber.keyboard) 
		{
			_horizontalAxis = "Horizontal";
			_verticalAxis = "Vertical";
		}
		else if (joyStickNumber == JoystickNumber.one) 
		{
			_horizontalAxis = "Joystick1_Horizontal";
			_verticalAxis = "Joystick1_Vertical";
		}
	}

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		#region Testing controller input
		joystickFloat = Input.GetAxis ("Horizontal");

		if (Input.GetKeyDown (KeyCode.Joystick1Button0)) 
		{
			Debug.Log ("You pressed Joystick1Button0! (it's probably A on the controller)");
		}
		if (Input.GetKeyDown (KeyCode.Joystick1Button1)) 
		{
			Debug.Log ("You pressed Joystick1Button1! (it's probably B on the controller)");
		}
		if (Input.GetKeyDown (KeyCode.Joystick1Button2)) 
		{
			Debug.Log ("You pressed Joystick1Button2! (it's probably X on the controller)");
		}
		if (Input.GetKeyDown (KeyCode.Joystick1Button3)) 
		{
			Debug.Log ("You pressed Joystick1Button3! (it's probably Y on the controller)");
		}
		#endregion
	}

	void FixedUpdate ()
	{
		Vector2 _movement = new Vector2 (Input.GetAxisRaw(_horizontalAxis), Input.GetAxisRaw(_verticalAxis));
		_movement.Normalize ();
		_rigidbody.velocity = _movement * _fMoveSpeed;

		_velocity = _rigidbody.velocity;
		_magnitude = _rigidbody.velocity.magnitude;
	}
}
