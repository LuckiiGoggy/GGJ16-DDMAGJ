using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class JukeBox : MonoBehaviour {

    public List<Player> m_Players;
    public AudioClip m_GodModeMusic;
    public AudioClip m_NormMusics;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GodExists())
            SwitchMusic(m_GodModeMusic);
        else
            SwitchMusic(m_NormMusics);


	}

    void SwitchMusic(AudioClip clip)
    {
        if (GetComponent<AudioSource>().clip != clip)
        {
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().Play();
        }
    }

    bool GodExists()
    {
        bool godExists = true;
        bool godDoesntExist = false; 
        foreach(Player player in m_Players)
        {
            if (player.m_IsInGodMode) return godExists;
        }

        return godDoesntExist;
    }
}
