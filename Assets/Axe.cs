using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour {
    private Animator _anim;
    public enum AxeState
    {
        DEFAULT=0,
        INHAND,
        INAIR
    }
    public AxeState axeState = AxeState.DEFAULT;
	// Use this for initialization
	void Awake () {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (axeState == AxeState.INAIR)
        {
            _anim.SetBool("Axe",true);
            Debug.Log("Play");
        }
        else
        {
            _anim.SetBool("Axe", false);
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Debug.Log("Ground");
            axeState = AxeState.DEFAULT;
        }   
    }
}
