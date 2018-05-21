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
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        StartCoroutine("WaitSeconds");
        GameManager.instance.StartGame(3);
    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(7.0f);
    }
}
