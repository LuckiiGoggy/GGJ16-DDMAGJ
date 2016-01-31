using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	public float m_animationsSpeed = 1;

	public Animator animator;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		animator.speed = m_animationsSpeed;
	}
}
