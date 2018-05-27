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
            GameManager.instance.gameType = GameManager.GameType.Start;
            WaitSecond(5);
            GameManager.instance.StartGame(3);
        }
    }
    protected void WaitSecond(int t)
    {
        StartCoroutine(IWaitSeconds(t));
    }
    protected IEnumerator IWaitSeconds(int t)
    {
        for (int i = 0; i <= t; i++)
        {
            Debug.Log("<color=red>Coroutine</color>");
            yield return new WaitForSeconds(1);
        }
            Debug.Log("<color=red>Done</color>");
    }
}