using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase:MonoBehaviour  {
    public enum UIType
    {
        Start,
        Exit
    }
    public GameObject text;
    public int index;
    public UIType uiType;
    public GameObject owner;
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Weapon")
        {
            //Debug.Log("Weapon Collider!");
            //BeginManager.instance.rotateObjectList.RemoveAt(index);
            //for (int i = index; i < BeginManager.instance.rotateObjectList.Count; i++)
            //{
            //    //让大于本index的UIBase中的index-1
            //    BeginManager.instance.uiBaseList[index+1].index--;
            //}
            //BeginManager.instance.uiBaseList.Remove(this);
            //Destroy(this.transform.parent.gameObject, 1f);
            BeginManager.instance.DestroyUIAll();
        }



    }
    protected virtual void Awake()
    {
        text = this.transform.parent.Find("text").gameObject;
    }
}
