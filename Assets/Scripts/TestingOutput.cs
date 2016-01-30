using UnityEngine;
using System.Collections;

public class TestingOutput : MonoBehaviour {

	// this is for testing to make sure we are getting input from controller
	public float joystickFloatX;
	public float joystickFloatY;

	// this is used to measure the player's velocity
	public Vector2 _velocity;
	// this is used to measure the player's magnitude
	public float _magnitude;

	private Rigidbody2D _rigidbody;

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update ()
	{
		#region Testing controller input
		joystickFloatX = Input.GetAxis ("Joystick1_Horizontal");
		joystickFloatY = Input.GetAxis ("Joystick1_Vertical");

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
		_velocity = _rigidbody.velocity;
		_magnitude = _rigidbody.velocity.magnitude;
	}
}
