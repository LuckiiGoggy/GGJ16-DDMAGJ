using UnityEngine;
using System.Collections;

public class AIPlayerMovement : PlayerMovement
{

	private float _fCurrentMoveSpeed = 1;
    private float lastMoveHor;
    private float lastMoveVer;

    private Rigidbody2D _rigidbody;
	private PlayerAnimation playerAnimation;

	void Start ()
	{
		_rigidbody = GetComponent<Rigidbody2D> ();
		playerAnimation = GetComponent<PlayerAnimation> ();
	}

	void FixedUpdate ()
	{
        return;
	}

    public void moveTowards(float gotoX, float gotoY)
    {
        Transform _transform = GetComponent<Transform>();
        Vector2 _position = _transform.position;
        float horVar = 0;
        float vertVar = 0;
        Vector2 displacement = _position - new Vector2(gotoX, gotoY);
        Vector2 displacementN = new Vector2(gotoX, gotoY) - _position;
        displacementN = (displacementN * 0.5f) + (new Vector2(lastMoveHor, lastMoveVer) * 0.5f);

        horVar = displacementN.x;
        vertVar = displacementN.y;

        if (horVar == 0 && vertVar == 0)
        {
            playerAnimation.animator.SetBool("Walk", false);
        }
        else
        {
            playerAnimation.animator.SetBool("Walk", true);
        }

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
            AIPlayer player = GetComponent<AIPlayer>();
            player.makeDecision();
        }

        lastMoveHor = hor;
        lastMoveVer = ver;

        float angle = Mathf.Atan2(-hor, ver) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

    }

    private void SlowPlayer ()
	{
		StartCoroutine (SlowPlayerTimer ());
	}

	IEnumerator SlowPlayerTimer ()
	{
		_fCurrentMoveSpeed /= 2;
		yield return new WaitForSeconds (_fSlowPlayerTimer);
		_fCurrentMoveSpeed *= 2;
	}

	private void StunPlayer ()
	{
		StartCoroutine (StunPlayerTimer ());
	}

	IEnumerator StunPlayerTimer ()
	{
		_fCurrentMoveSpeed = 0;
		yield return new WaitForSeconds (_fStunPlayerTimer);
		_fCurrentMoveSpeed = _fMoveSpeed;
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.name == "slow-box") 
		{
			SlowPlayer ();
		}
		else if (other.gameObject.name == "stun-box") 
		{
			StunPlayer ();
		}
	}
}
