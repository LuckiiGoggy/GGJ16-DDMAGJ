using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// these are the controller numbers:
	// 0 = keyboard
	// 1 = controller1
	// etc.
	public int _iControllerNumber;

	private PlayerMovement _playerMovement_ref;
	private PlayerRotation _playerRotation_ref;

	private Player m_Player;

	void Awake ()
	{

		// first get the playermovement and playerrotation on THIS script's gameobject
		_playerMovement_ref = GetComponent<PlayerMovement> ();
		_playerRotation_ref = GetComponent<PlayerRotation>();
		// and the player
		m_Player = GetComponent<Player>();

		// then tell the inputcontrollermanager to set its static variables to match
		// THIS script's _iControllerNumber,
		InputControllerManager.WhatControllerAmI (_iControllerNumber);
		// THEN set THIS script's movement and rotation script axis values to match the inputcontroller's values
		// [playermovement]
		_playerMovement_ref._horizontalAxis = InputControllerManager._horizontalAxis;
		_playerMovement_ref._verticalAxis = InputControllerManager._verticalAxis;
		// [playerrotation]
		_playerRotation_ref._horizontalAxis = InputControllerManager._horizontalAxis;
		_playerRotation_ref._verticalAxis = InputControllerManager._verticalAxis;

		m_Player.m_Attack = InputControllerManager._button01;
	}
}
