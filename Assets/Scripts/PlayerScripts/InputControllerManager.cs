using UnityEngine;
using System.Collections;

public class InputControllerManager : MonoBehaviour {

	public static string _horizontalAxis;
	public static string _verticalAxis;

	public static void WhatControllerAmI (int joystickNumber)
	{
		if (joystickNumber == 0) 
		{
			_horizontalAxis = "Horizontal";
			_verticalAxis = "Vertical";
		}
		else if (joystickNumber == 1) 
		{
			_horizontalAxis = "Joystick1_Horizontal";
			_verticalAxis = "Joystick1_Vertical";
		}
		else if (joystickNumber == 2) 
		{
			_horizontalAxis = "Joystick2_Horizontal";
			_verticalAxis = "Joystick2_Vertical";
		}
		else if (joystickNumber == 3) 
		{
			_horizontalAxis = "Joystick3_Horizontal";
			_verticalAxis = "Joystick3_Vertical";
		}
		else if (joystickNumber == 4) 
		{
			_horizontalAxis = "Joystick4_Horizontal";
			_verticalAxis = "Joystick4_Vertical";
		}
	}
}
