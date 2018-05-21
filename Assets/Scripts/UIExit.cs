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
}
