using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

	// these are the controller numbers:
	// 0 = keyboard
	// 1 = controller1
	// etc.
	public int _iControllerNumber;
	private InputControllerManager.KeyMap input;
    public bool m_isAxisInUse;


    void Awake ()
	{
		// then tell the inputcontrollermanager to set its static variables to match
		// THIS script's _iControllerNumber,
		input = InputControllerManager.WhatControllerAmI (_iControllerNumber);
	}

	void Update ()
	{
        MenuBase Mmenubase = GetComponent<MenuBase>();
        int menuState = Mmenubase._menuState;
        if (Input.GetKey(input._button01)) {
            Mmenubase.LoadLevel();
		}
        float vertAxis = Input.GetAxisRaw(input._verticalAxis);
        if (vertAxis != 0)
        {
            if (m_isAxisInUse == false)
            {
                // Call your event function here.
                if (vertAxis > 0)
                {
                    if (menuState != 4)
                    {
                        Mmenubase._menuState = (menuState + 1);
                    }
                }

                if (vertAxis < 0)
                {
                    if (menuState != 1)
                    {
                        Mmenubase._menuState = (menuState - 1);
                    }
                }

                m_isAxisInUse = true;
            }
        }
        if (vertAxis == 0)
        {
            m_isAxisInUse = false;

        }
	}
}
