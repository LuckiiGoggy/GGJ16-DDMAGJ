using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour {

    public int m_RequiredSacrifices;
    private int m_CurrentSacrificeCount;

    private Player player;


    public void Start()
    {
        player = GetComponent<Player>();
    }

    public void FixedUpdate()
    {
        if(m_CurrentSacrificeCount >= m_RequiredSacrifices)
        {
            player.GodModeOn();
        }
        else
        {
            player.GodModeOff();
        }
    }

    public void AddSacrifice()
    {
        m_CurrentSacrificeCount += m_RequiredSacrifices;
    }
}
