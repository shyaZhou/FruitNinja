using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExit : UIBase {

    public UIExit()
    {
        
    }
    protected override void Awake()
    {
        base.Awake();
        text.GetComponent<SpriteRenderer>().sprite = BeginManager.instance.exitSprite;
    }
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == "Weapon")
        {
            
            Application.Quit();
        }
        //IWaitSeconds(3f);
    }


}
