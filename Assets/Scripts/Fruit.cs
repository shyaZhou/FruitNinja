using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= -10f)
        {
            if (GameManager.instance.hpObsList.Count > 0)
            {
                GameManager.instance.hp--;
                Destroy(GameManager.instance.hpObsList[GameManager.instance.hpObsList.Count-1]);
                GameManager.instance.hpObsList.RemoveAt(GameManager.instance.hpObsList.Count - 1);
                Destroy(this.gameObject);
            }
        }
	}
}
