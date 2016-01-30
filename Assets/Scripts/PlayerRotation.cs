using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	// depending on what type of JoystickNumber you chose, the following 2 strings get changed
	// to have names relative to their controller number's name in InputManager
	public string _horizontalAxis;
	public string _verticalAxis;

	void Update ()
	{
		if (Input.GetAxis (_horizontalAxis) != 0 || Input.GetAxis (_verticalAxis) != 0) {
			float angle = Mathf.Atan2 (-Input.GetAxis (_horizontalAxis), Input.GetAxis (_verticalAxis)) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0, 0, angle);
		}
	}
}
