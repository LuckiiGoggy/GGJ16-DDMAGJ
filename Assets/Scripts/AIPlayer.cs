using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AIPlayer : Player {
    
    public float _offset = 0.3f;
    private GameObject m_Sacrifice;

    #region Power States

    public GameObject m_target;
    public GameObject m_base;
    public Vector2 _movement;
    private Rigidbody2D _rigidbody;
    public bool restrictMovement;
    public int restrictTarget;
    public Vector2 finalPos;
    private string movetyp;
    private float test;

    private Animator animator;
    #endregion

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        restrictMovement = false;
        m_IdleSprite = GetComponent<SpriteRenderer> ().sprite;

		m_DebuffTimers = new Dictionary<Debuffs, float>();
		m_PowerStateTimers = new Dictionary<PowerStates, float>();

		m_DebuffTimers.Add(Debuffs.Slow, 0);
		m_DebuffTimers.Add(Debuffs.Stun, 0);

		m_PowerStateTimers.Add(PowerStates.Invulnerability, 0);
		m_PowerStateTimers.Add(PowerStates.SuperSpeed, 0);
        
		PlayerMovement control = gameObject.GetComponent<PlayerMovement>();
		control._fMoveSpeed = m_MovementSpeed;

		animator = GetComponent<Animator> ();
    }


	
	// Update is called once per frame
	void FixedUpdate () {

        if (restrictMovement)
        {
            return;
        }



        if (GetComponent<Player>().m_IsInGodMode)
        {
            if (restrictTarget == 0)
            {
                m_target = FindPlayer();
                attackPlayer();
            } else
            {
                attackPlayer();
            }
        }


        else
        {
            m_target = FindSacrifice();
            if (m_target != null)
            {
                getSacrifice();
            }
            else
            {
                m_target = FindItem();
                if (m_target != null)
                {
                    getSacrifice();
                }
            }
        }

    }

    public void attackPlayer()
    {
        AIPlayerMovement movement = GetComponent<AIPlayerMovement>();
        
        if (m_target.tag == "Player")
        {
            movement.moveTowards(m_target.transform.position.x, m_target.transform.position.y);
            if ((m_target.transform.position - transform.position).magnitude < 1)
            {
                animator.SetTrigger("Attack");
                makeDecision(0, 1);
            }
        }
    }

    public void getSacrifice()
    {

        Vector2 basePosition = m_base.transform.position;
        Vector2 sacrificePosition = m_target.transform.position;
        Transform _transform = GetComponent<Transform>();
        Vector2 _position = _transform.position;

        float baseToPlayerd = Vector2.Distance(basePosition, _position);
        float baseToSacrificed = Vector2.Distance(basePosition, sacrificePosition);

        float destinationx = _position.x;
        float destinationy = _position.y;

        Vector2 baseToSacrificedV = sacrificePosition - basePosition;
        Vector2 baseToPlayerV = _position - basePosition;
        baseToSacrificedV.Normalize();

        float calculation = Vector2.Dot((_position - basePosition), baseToSacrificedV);
        Vector2 xpoint = basePosition + (baseToSacrificedV * calculation);
        Vector2 perpendicular = xpoint - _position;
        Vector2 perpendicularNormal = perpendicular.normalized;

        Vector2 neededVector = _position - sacrificePosition;
        float angle = Vector2.Angle(neededVector, baseToSacrificedV);
        float angle2 = Vector2.Angle(baseToSacrificedV, new Vector2(0, 1));
        float angle4 = Vector2.Angle(baseToSacrificedV, new Vector2(1, 0));
        float angle3 = Vector2.Angle(baseToPlayerV, new Vector2(0, 1));
        float angleDiff = Mathf.Abs(angle3 - angle2);
        float randomNumberA = Random.Range(0.8f, 2);
        test = angleDiff;

        if (baseToPlayerd <= baseToSacrificed)
        {
            finalPos = sacrificePosition - (perpendicularNormal * 1);
            movetyp = "move and sidestep";
        }
        else if (baseToPlayerd <= (baseToSacrificed + 1) && (angle2 <= 25) && (angleDiff > 2))
        {

            finalPos = _position + new Vector2(0, 2) + (perpendicularNormal * randomNumberA);
            movetyp = "stuck on wall";
        }

        else if (baseToPlayerd <= (baseToSacrificed + 1) && (angle2 >= 155) && (angleDiff > 2))
        {
            finalPos = _position - new Vector2(0, 2) + (perpendicularNormal * randomNumberA);
            movetyp = "stuck on wall";
        }

        else if (baseToPlayerd <= (baseToSacrificed + 1) && (angle4 <= 25) && (angleDiff > 2))
        {
            finalPos = _position + new Vector2(2, 0) + (perpendicularNormal * randomNumberA);
            movetyp = "stuck on wall";
        }

        else if (baseToPlayerd <= (baseToSacrificed + 1) && (angle4 >= 155) && (angleDiff > 2))
        {
            finalPos = _position - new Vector2(2, 0) + (perpendicularNormal * randomNumberA);
            movetyp = "stuck on wall";
        }

        else if (baseToPlayerd <= (baseToSacrificed + 0.1f) && angle > 45)
        {
            float randomNumber = Random.Range(0, 0.5f);
            finalPos = sacrificePosition - (perpendicularNormal * randomNumber) + (baseToSacrificedV * _offset);
            movetyp = "beside box";
        }
        else
        {
            finalPos = sacrificePosition;
            movetyp = "push towards base";
        }


        destinationx = finalPos.x;
        destinationy = finalPos.y;

        

        AIPlayerMovement movement = GetComponent<AIPlayerMovement>();
        movement.moveTowards(destinationx, destinationy);

    }



    //private void moveWait()
    //{
    //    StartCoroutine(moveWaitTimer());
    //}

    //IEnumerator moveWaitTimer()
    //{
    //    restrictMovement = 1;
    //    _waitTime = Random.Range(0, 0.6f);
    //    yield return new WaitForSeconds(_waitTime);
    //    restrictMovement = 0;
    //}

    public void makeDecision(float min, float max)
    {
        StartCoroutine(makeDecisionTimer(min, max));
    }

    IEnumerator makeDecisionTimer(float min, float max)
    {
        restrictMovement = false;
        float randomNumber = Random.Range(min, max);
        yield return new WaitForSeconds(randomNumber);
        print("return ");
        restrictMovement = true;
    }

    public void decidedTarget()
    {
        StartCoroutine(decidedTargetTimer());
    }

    IEnumerator decidedTargetTimer()
    {
        restrictTarget = 1;
        float randomNumber = Random.Range(4, 7);
        yield return new WaitForSeconds(randomNumber);
        restrictTarget = 0;
    }

    GameObject FindSacrifice()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Sacrifice");
        if (gos.Length == 0)
        {
            return null;
        }
        else {
            return gos[0];
        }
    }

    GameObject FindItem()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Item");
        if (gos.Length == 0)
        {
            return null;
        }
        else {
            return gos[0];
        }
    }

    GameObject FindPlayer()
    {
        GameObject[] gos;
        Transform _transform = GetComponent<Transform>();
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = _transform.position;
        gos = GameObject.FindGameObjectsWithTag("Player");
        if (gos.Length == 0)
        {
            return null;
        }
        else {

            foreach (GameObject go in gos)
            {
                if (go.transform.position != position)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
            }
            return closest;
        }
    }

}
