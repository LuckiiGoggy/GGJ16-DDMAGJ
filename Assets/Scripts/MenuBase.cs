using UnityEngine;
using System.Collections;

public class MenuBase : MonoBehaviour {
    public int _menuState = 0;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
      
    }
    // load correct level for _menuState
    public void LoadLevel()
    {
        if (_menuState <= 0)
        {
            return;
        } else if (_menuState == 1)
        {
            Application.LoadLevel("Scene1p");
        }
        else if (_menuState == 2)
        {
            Application.LoadLevel("Scene2p");
        }
        else if (_menuState == 3)
        {
            Application.LoadLevel("Scene3p");
        }
        else
        {
            Application.LoadLevel("Scene01");
        }
    }
}



