using UnityEngine;
using System.Collections;

public class InputControllerManager : MonoBehaviour {

	public static string _horizontalAxis;
	public static string _verticalAxis;
    public static KeyCode _button01;

	public static void WhatControllerAmI (int joystickNumber)
	{
		if (joystickNumber == 0) 
		{
			_horizontalAxis = "Horizontal";
			_verticalAxis = "Vertical";
            _button01 = KeyCode.Space;
            
		}
		else if (joystickNumber == 1) 
		{
			_horizontalAxis = "Joystick1_Horizontal";
			_verticalAxis = "Joystick1_Vertical";
            _button01 = KeyCode.Joystick1Button0;
        }
		else if (joystickNumber == 2) 
		{
			_horizontalAxis = "Joystick2_Horizontal";
            _verticalAxis = "Joystick2_Vertical";
            _button01 = KeyCode.Joystick2Button0;
		}
		else if (joystickNumber == 3) 
		{
			_horizontalAxis = "Joystick3_Horizontal";
            _verticalAxis = "Joystick3_Vertical";
            _button01 = KeyCode.Joystick3Button0;
		}
		else if (joystickNumber == 4) 
		{
			_horizontalAxis = "Joystick4_Horizontal";
            _verticalAxis = "Joystick4_Vertical";
            _button01 = KeyCode.Joystick4Button0;
		}
	}
}
