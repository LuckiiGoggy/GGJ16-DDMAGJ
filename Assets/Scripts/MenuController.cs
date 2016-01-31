using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

	// these are the controller numbers:
	// 0 = keyboard
	// 1 = controller1
	// etc.
	public int _iControllerNumber;

	public List<MenuTotemPart> m_MenuTotemParts;

	private float m_PreviousAxis;
	private int m_Current;

	void Awake ()
	{
		// then tell the inputcontrollermanager to set its static variables to match
		// THIS script's _iControllerNumber,
		InputControllerManager.WhatControllerAmI (_iControllerNumber);
	}

	void Update ()
	{
		if (Input.GetKey(InputControllerManager._button01)) {
			print ("start game");
			Application.LoadLevel("Scene01");
		}

		if (Input.GetAxisRaw(InputControllerManager._verticalAxis) > 0 && !(m_PreviousAxis > 0)) {
			m_Current = Math.Min(++m_Current, m_MenuTotemParts.Count - 1);
		}

		if (Input.GetAxisRaw(InputControllerManager._verticalAxis) < 0 && !(m_PreviousAxis < 0)) {
			m_Current = Math.Max(--m_Current, 0);
		}

		m_MenuTotemParts [m_Current].OnMouseOver ();

		m_PreviousAxis = Input.GetAxisRaw (InputControllerManager._verticalAxis);
	}
}
