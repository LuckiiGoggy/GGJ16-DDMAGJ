using UnityEngine;
using System.Collections;

public class AIPlayer : MonoBehaviour {

    private float _fCurrentMoveSpeed = 1;
    private float _waitTime = 0.0f;
    public float _offset = 0.3f;
    public float _fMoveSpeed = 1;
    public float _fSlowPlayerTimer = 3.0f;
    public float _fStunPlayerTimer = 3.0f;

    public GameObject m_sacrifice;
    public GameObject m_base;
    public Vector2 _movement;
    private Rigidbody2D _rigidbody;
    public int restrictMovement;
    public Vector2 finalPos;
    private float lastMoveHor;
    private float lastMoveVer;
    public string movetyp;
    public float test;


    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
        restrictMovement = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {


        if (restrictMovement == 1)
        {
            return;
        }

        getSacrifice();

        //attackPlayer();
    }

    public void attackPlayer()
    {
        return;
    }

    public void getSacrifice()
    {

        Vector2 basePosition = m_base.transform.position;
        Vector2 sacrificePosition = m_sacrifice.transform.position;
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

        moveTowards(destinationx, destinationy);


    }

    public void moveTowards(float gotoX, float gotoY) {
        Transform _transform = GetComponent<Transform>();
        Vector2 _position = _transform.position;
        float horVar = 0;
        float vertVar = 0;
        Vector2 displacement = _position -  new Vector2 (gotoX, gotoY);
        Vector2 displacementN = new Vector2(gotoX, gotoY) - _position;
        displacementN = (displacementN * 0.5f) + (new Vector2(lastMoveHor, lastMoveVer) * 0.5f);

        horVar = displacementN.x;
        vertVar = displacementN.y;


        AIUpdate(horVar, vertVar);
    }

    //Updates movement, rotation.
    public void AIUpdate(float hor, float ver)
    {
        if (Random.Range(0, 100) < 20)
        {

            print("stop");
            _movement = new Vector2(0, 0);
        }
        else
        {
            _movement = new Vector2(hor, ver);
            _movement.Normalize();
        }

        _rigidbody.velocity = _movement * _fMoveSpeed;
        if (lastMoveHor != hor || lastMoveVer != ver)
        {
            makeDecision();
        }

        lastMoveHor = hor;
        lastMoveVer = ver;
        
        float angle = Mathf.Atan2(-hor, ver) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
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

    private void makeDecision()
    {
        StartCoroutine(makeDecisionTimer());
    }

    IEnumerator makeDecisionTimer()
    {
        restrictMovement = 1;
        float randomNumber = Random.Range(0, 0.01f);
        yield return new WaitForSeconds(randomNumber);
        restrictMovement = 0;
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
