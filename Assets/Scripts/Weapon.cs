using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    public bool m_IsAttacking;

	public float m_AttackWaitTimer = 0.5f;
	public float m_WeaponTriggerTimer = 0.25f;

	public float m_GodAttackWaitTimer = 0.8f;
	public float m_GodWeaponTriggerTimer = 0.5f;

    public float m_StunLength;
    public float m_SlowLength;

	public bool m_IsGodWeapon;

	private PlayerAnimation playerAnimation;
	private BoxCollider2D weaponTrigger;

	// Use this for initialization
	void Start () {
		playerAnimation = GetComponentInParent<PlayerAnimation> ();
		weaponTrigger = GetComponent<BoxCollider2D> ();
	}

	public void SetGodWeapon()
	{
		m_IsGodWeapon = true;
	}
	public void ReverseGodWeapon()
	{
		m_IsGodWeapon = false;
	}

	public bool IsGodWeapon()
	{
		return m_IsGodWeapon;
	}

    public void Attack()
    {
        Debug.Log("Attack");
		if (m_IsGodWeapon == false) {
			if (!m_IsAttacking) {
				playerAnimation.animator.SetTrigger ("Attack");

				m_IsAttacking = true;
				StartCoroutine (EnableWeaponTriggerTimer ());
				StartCoroutine (AttackWaitTimer ());
			}
		} else {
			if (!m_IsAttacking) {
				playerAnimation.animator.SetTrigger ("GodAttack");

				m_IsAttacking = true;
				StartCoroutine (EnableGodWeaponTriggerTimer ());
				StartCoroutine (GodAttackWaitTimer ());
			}
		}
    }



	IEnumerator EnableWeaponTriggerTimer ()
	{
		yield return new WaitForSeconds (m_WeaponTriggerTimer);
		weaponTrigger.enabled = true;
		yield return new WaitForSeconds (m_WeaponTriggerTimer);
		weaponTrigger.enabled = false;
	}

	IEnumerator AttackWaitTimer ()
	{
		yield return new WaitForSeconds (m_AttackWaitTimer);
		m_IsAttacking = false;
	}

	IEnumerator EnableGodWeaponTriggerTimer ()
	{
		yield return new WaitForSeconds (m_GodWeaponTriggerTimer);
		weaponTrigger.enabled = true;
		yield return new WaitForSeconds (m_GodWeaponTriggerTimer);
		weaponTrigger.enabled = false;
	}

	IEnumerator GodAttackWaitTimer ()
	{
		yield return new WaitForSeconds (m_GodAttackWaitTimer);
		m_IsAttacking = false;
	}
}
