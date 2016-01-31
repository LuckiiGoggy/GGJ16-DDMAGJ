using UnityEngine;
using System.Collections;

/// <summary>
/// Power up defines the modifications that is done of a game object when
/// the player picks it up. It currently assumes that a single powerup only 
/// affects one power state in the player.
/// </summary>
public class PowerUp : MonoBehaviour {


    /// <summary>
    /// The power state that this power up modifies
    /// </summary>
    public Player.PowerStates m_TargetPlayerPowerStates;

    /// <summary>
    /// The value that the power up modifies the targeted power state
    /// </summary>
    public float m_Modifier;

    /// <summary>
    /// The length of how long this power up will last
    /// </summary>
    public float m_Duration;

    void OnTriggerEnter2D(Collider2D coll)
    {
        Base playerBase = coll.GetComponent<Base>();
        if (playerBase != null)
        {
            playerBase.m_Owner.ApplyPowerUp(m_TargetPlayerPowerStates, m_Modifier, m_Duration);
            Destroy(this.gameObject);
        }

    }
}
