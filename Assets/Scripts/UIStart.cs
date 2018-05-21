using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : UIBase {
    public GameObject spwaner;
    private void Awake()
    {
        spwaner = GameObject.Find("GamePlay").transform.Find("Spawner").gameObject;
        Debug.Log(spwaner);
    }
    public UIStart() 
    {
      
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        spwaner.SetActive(true);
    }


}
