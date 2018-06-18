using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {
    public bool isHurt=false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= -10f)
        {
            Hurt();
            Destroy(this.gameObject);
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            if (isHurt == false)
            {
                Hurt();
            }
        }
        else if (collision.transform.tag == "Weapon")
        {
            isHurt = true;
            if (collision.transform.GetComponent<SwordCutter>() != null)
            {
                collision.transform.GetComponent<SwordCutter>().pieces[0].GetComponent<Fruit>().isHurt = true;
            }
        }
        Destroy(this.gameObject, 3f);
    }
    private void Hurt()
    {
        if (GameManager.instance.hpObsList.Count > 0)
        {
            GameManager.instance.hp--;
            Destroy(GameManager.instance.hpObsList[GameManager.instance.hpObsList.Count - 1]);
            GameManager.instance.hpObsList.RemoveAt(GameManager.instance.hpObsList.Count - 1);
        }
    }
}
