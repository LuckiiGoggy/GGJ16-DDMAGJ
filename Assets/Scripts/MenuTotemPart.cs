using UnityEngine;
using System.Collections;

public class MenuTotemPart : MonoBehaviour {
    public MenuBase _menuBase;
    public int level;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        int menuState = _menuBase._menuState;
        
        if (level <= menuState)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("isActive", true);

        } else {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("isActive", false);
        }
    }

    void OnMouseOver()
    {
        _menuBase._menuState = level;
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isActive", true);
    }

    void OnMouseExit()
    {
        if (_menuBase._menuState == level)
        {
            _menuBase._menuState = 0;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("isActive", false);
        }
    }
    
    void OnMouseDown()
    {
        Application.LoadLevel("Scene01");
    }
}
