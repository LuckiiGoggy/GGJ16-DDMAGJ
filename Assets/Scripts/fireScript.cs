using UnityEngine;
using System.Collections;

public class fireScript : MonoBehaviour {

    // Use this for initialization
    public bool isBack;

    void Start () {
        if (isBack)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("back", true);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
