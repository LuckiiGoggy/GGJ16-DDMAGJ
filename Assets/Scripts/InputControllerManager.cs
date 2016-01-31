using UnityEngine;
using System.Collections;

public class InputControllerManager : MonoBehaviour
{

    public struct KeyMap
    {
        public string _horizontalAxis;
        public string _verticalAxis;
        public KeyCode _button01;
        public KeyCode _buttonPause;
    }

    public static KeyMap WhatControllerAmI(int joystickNumber)
    {
        KeyMap output = new KeyMap();

        if (joystickNumber == 0)
        {
            output._horizontalAxis = "Horizontal";
            output._verticalAxis = "Vertical";
            output._button01 = KeyCode.Space;
            output._buttonPause = KeyCode.Escape;

        }
        else if (joystickNumber == 1)
        {
            output._horizontalAxis = "Joystick1_Horizontal";
            output._verticalAxis = "Joystick1_Vertical";
            output._button01 = KeyCode.Joystick1Button0;
            output._buttonPause = KeyCode.Joystick1Button7;
        }
        else if (joystickNumber == 2)
        {
            output._horizontalAxis = "Joystick2_Horizontal";
            output._verticalAxis = "Joystick2_Vertical";
            output._button01 = KeyCode.Joystick2Button0;
            output._buttonPause = KeyCode.Joystick2Button7;
        }
        else if (joystickNumber == 3)
        {
            output._horizontalAxis = "Joystick3_Horizontal";
            output._verticalAxis = "Joystick3_Vertical";
            output._button01 = KeyCode.Joystick3Button0;
            output._buttonPause = KeyCode.Joystick3Button7;
        }
        else if (joystickNumber == 4)
        {
            output._horizontalAxis = "Joystick4_Horizontal";
            output._verticalAxis = "Joystick4_Vertical";
            output._button01 = KeyCode.Joystick4Button0;
            output._buttonPause = KeyCode.Joystick4Button7;
        }

        return output;
    }
}
