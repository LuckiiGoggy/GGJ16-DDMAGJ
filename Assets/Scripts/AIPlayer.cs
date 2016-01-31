using UnityEngine;
using System.Collections;

public class AIPlayer : MonoBehaviour {

    private float _fCurrentMoveSpeed = 1;
    private float _waitTime = 0.09f;
    public float _offset = 0.1f;
    public float _fMoveSpeed = 1;
    public float _fSlowPlayerTimer = 3.0f;
    public float _fStunPlayerTimer = 3.0f;

    public GameObject m_sacrifice;
    public GameObject m_base;
    public Vector2 _movement;
    private Rigidbody2D _rigidbody;
    private int restrictMovement;
    public Vector2 finalPos;
    private int lastMoveHor;
    private int lastMoveVer;


    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        restrictMovement = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 basePosition = m_base.transform.position;
        Vector2 sacrificePosition = m_sacrifice.transform.position;
        Transform _transform = GetComponent<Transform>();
        Vector2 _position = _transform.position;

        float baseToPlayerd = Vector2.Distance(basePosition, _position);
        float baseToSacrificed = Vector2.Distance(basePosition, sacrificePosition);


        float destinationx = _position.x;
        float destinationy = _position.y;

        Vector2 baseToSacrificedV = sacrificePosition - basePosition;
        baseToSacrificedV.Normalize();



        float calculation = Vector2.Dot((_position - basePosition), baseToSacrificedV);
        Vector2 xpoint = basePosition + (baseToSacrificedV * calculation);
        Vector2 perpendicular = xpoint - _position;
        Vector2 perpendicularNormal = perpendicular.normalized;

        Vector2 neededVector = _position - sacrificePosition;
        float angle = Vector2.Angle(neededVector, baseToSacrificedV);

        if (baseToPlayerd <= baseToSacrificed)
        {
            finalPos = sacrificePosition - (perpendicularNormal * 1);
        }
        else if (baseToPlayerd <= (baseToSacrificed + _offset) && angle > 45)
        {
            finalPos = sacrificePosition - (perpendicularNormal * 0.5f) + (baseToSacrificedV * _offset);
        } else
        {
            finalPos = sacrificePosition;
        }


        destinationx = finalPos.x;
        destinationy = finalPos.y;

        float randomNumberx = Random.Range(0, 0.2f);
        float randomNumbery = Random.Range(0, 0.2f);
        moveTowards(destinationx + randomNumberx, destinationy + randomNumbery);


        
    }

    public void moveTowards(float gotoX, float gotoY) {
        Transform _transform = GetComponent<Transform>();
        Vector2 _position = _transform.position;
        int horVar = 0;
        int vertVar = 0;
        Vector2 displacement = _position -  new Vector2 (gotoX, gotoY);
        float anglefromright = Vector2.Angle(displacement, new Vector2(1, 0));
        float anglefromup = Vector2.Angle(displacement, new Vector2(0, 1));

        if (0 <= anglefromright && anglefromright < 67.5)
        {
            horVar = -1;
        } else if (anglefromright > 112.5)
        {
            horVar = 1;
        }


        if (0 <= anglefromup && anglefromup < 67.5)
        {
            vertVar = -1;
        }
        else if (anglefromup > 112.5)
        {
            vertVar = 01;
        }



        if (restrictMovement != 1)
        {
            AIUpdate(horVar, vertVar);
        }
    }

    //Updates movement, rotation.
    public void AIUpdate(int hor, int ver)
    {
        if (lastMoveHor != hor & lastMoveVer != ver)
        {
            _rigidbody.velocity = new Vector2(0, 0);
            makeDecision();
        }
        
        _movement = new Vector2(hor, ver);
        _movement.Normalize();
        _rigidbody.velocity = _movement * _fMoveSpeed;
        lastMoveHor = hor;
        lastMoveVer = ver;

        float angle = Mathf.Atan2(-hor, ver) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        moveWait();
    }

    private void moveWait()
    {
        StartCoroutine(moveWaitTimer());
    }

    IEnumerator moveWaitTimer()
    {
        restrictMovement = 1;
        yield return new WaitForSeconds(_waitTime);
        restrictMovement = 0;
    }

    private void makeDecision()
    {
        StartCoroutine(makeDecisionTimer());
    }

    IEnumerator makeDecisionTimer()
    {
        float randomNumber = Random.Range(6, 10);
        yield return new WaitForSeconds(randomNumber);
    }


    private void SlowPlayer()
    {
        StartCoroutine(SlowPlayerTimer());
    }

    IEnumerator SlowPlayerTimer()
    {
        _fCurrentMoveSpeed /= 2;
        yield return new WaitForSeconds(_fSlowPlayerTimer);
        _fCurrentMoveSpeed *= 2;
    }

    private void StunPlayer()
    {
        StartCoroutine(StunPlayerTimer());
    }

    IEnumerator StunPlayerTimer()
    {
        _fCurrentMoveSpeed = 0;
        yield return new WaitForSeconds(_fStunPlayerTimer);
        _fCurrentMoveSpeed = _fMoveSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "slow-box")
        {
            SlowPlayer();
        }
        else if (other.gameObject.name == "stun-box")
        {
            StunPlayer();
        }
    }
}
