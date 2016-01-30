using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    
    private float m_AnimationTimer;
    public float m_AnimationLength;
    public float m_AttackRange;

    public bool m_IsAttacking;

    public float m_StunLength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_IsAttacking)
        {
            m_AnimationTimer += Time.fixedDeltaTime;
        
            if(m_AnimationTimer >= m_AnimationLength)
            {
                m_IsAttacking = false;
            }
            else if (m_AnimationTimer >= m_AnimationLength / 2)
            {
                transform.Translate(Vector3.down * m_AttackRange / 2 * Time.fixedDeltaTime);
            }
            else if (m_AnimationTimer <= m_AnimationLength / 2)
            {
                transform.Translate(Vector3.up * m_AttackRange / 2 * Time.fixedDeltaTime);
            }
        }
	}

    public void Attack()
    {
        if(!m_IsAttacking)
        {
            m_IsAttacking = true;
            m_AnimationTimer = 0;
        }
    }
}
