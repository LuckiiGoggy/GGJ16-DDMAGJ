using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour {

    public float m_ApocalypseCountdown;

    // The delay in-between the meteor summons
    public Vector2 m_MeteorDelayRange;

    // The delay in-between the meteor summons
    public float m_MeteorDelayTime;

    // x = min x
    // y = max x
    // z = min y
    // w = max y
    public Vector4 m_SpawnBoundaries;

    // The meteor object that's spawned
    public Transform m_Meteor;

	// Use this for initialization
	void Start () {
        m_SpawnBoundaries = new Vector4();
        m_SpawnBoundaries.x = transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x / 2;
        m_SpawnBoundaries.y = transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2;
        m_SpawnBoundaries.z = transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2;
        m_SpawnBoundaries.w = transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_ApocalypseCountdown > 0)
            m_ApocalypseCountdown -= Time.fixedDeltaTime;
        else
            TriggerApocalypse();        
	}

    private void TriggerApocalypse()
    {
        if (m_MeteorDelayTime > 0)
            m_MeteorDelayTime -= Time.fixedDeltaTime;
        else
        {
            m_MeteorDelayTime = Random.Range(m_MeteorDelayRange.x, m_MeteorDelayRange.y);
            var x = Random.Range(m_SpawnBoundaries.x, m_SpawnBoundaries.y);
            var y = Random.Range(m_SpawnBoundaries.z, m_SpawnBoundaries.w);

            Vector3 loc = new Vector3(x, y, 0);

            Instantiate(m_Meteor, loc, Quaternion.identity);
        }
    }
}
