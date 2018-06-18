using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTest : MonoBehaviour {
    public bool isCollision;
    public Animation anim;
    private void Awake()
    {
        isCollision = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("<color=yellow>Enter</color>");
        isCollision = true;

    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("<color=yellow>Stay</color>");
        isCollision = true;

    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        StartCoroutine("Wait");
        isCollision = false;

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            anim.Play();
        }
    }
}
