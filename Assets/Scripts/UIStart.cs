using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : UIBase
{

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
            
            WaitSecond(1.5f);
            
        }
    }
    protected void WaitSecond(float t)
    {
        GameManager.instance.StartCoroutine(IWaitSeconds(t));
        //StartCoroutine(IWaitSeconds(t));
    }
    protected IEnumerator IWaitSeconds(float t)
    {
        
         yield return new WaitForSeconds(t);
        
        GameManager.instance.gameType = GameManager.GameType.Start;
        GameManager.instance.StartGame(3);
        GameManager.instance.StartTime(60f);

    }
}