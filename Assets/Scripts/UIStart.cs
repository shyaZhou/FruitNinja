using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : UIBase {
    public GameObject spwaner;
    protected override void Awake()
    {
        base.Awake();
        text.GetComponent<SpriteRenderer>().sprite = BeginManager.instance.startSprite;
        //spwaner = GameObject.Find("GamePlay").transform.Find("Spawner").gameObject;
    }
    public UIStart() 
    {

    }
    //public override void OnCollisionEnter(Collision collision)
    //{
    //    base.OnCollisionEnter(collision);
    //    if (collision.collider.tag == "Weapon")
    //    {
    //    StartCoroutine("WaitSeconds");
    //    GameManager.instance.StartGame(100);
    //    }
    //}
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Weapon")
        {
            StartCoroutine("WaitSeconds");
            GameManager.instance.StartGame(100);
        }
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(7.0f);
    }
}
