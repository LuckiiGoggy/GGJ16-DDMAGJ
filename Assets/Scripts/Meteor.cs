using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{

    public Vector2 toBeSize;
    public float m_timeToChangeSize;

    public float m_enableColliderAfterTime;

    public float m_deathTimer;

    public CircleCollider2D collider;

    void Start()
    {
        Destroy(gameObject, m_deathTimer);

        collider = GetComponent<CircleCollider2D>();
        StartCoroutine(EnableColliderAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector2.Lerp(transform.localScale, toBeSize, m_timeToChangeSize);
    }

    IEnumerator EnableColliderAfterTime()
    {
        yield return new WaitForSeconds(m_enableColliderAfterTime);
        collider.enabled = true;
    }
}
