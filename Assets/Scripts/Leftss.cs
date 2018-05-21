using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Leftss : MonoBehaviour
{
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    public Transform sphere;//重置功能
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Touch Trigger");
        }
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("TouchDown Trigger");
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("TouchUp Trigger");
        }
        //把物体拖入实现重置功能
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            sphere.transform.position = transform.position;
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
    void OnTriggerStay(Collider collider)
    {
        //物品抓取
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            collider.attachedRigidbody.isKinematic = true;
            collider.gameObject.transform.SetParent(gameObject.transform);
        }
        //物品分离
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            collider.attachedRigidbody.isKinematic = false;
            collider.gameObject.transform.SetParent(null);
            tossObject(collider.attachedRigidbody);
        }
    }
    //物品投掷
    void tossObject(Rigidbody rigidbody)
    {
        Transform orgin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (orgin != null)
        {
            rigidbody.velocity = orgin.TransformVector((device.velocity));
            rigidbody.angularVelocity = orgin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidbody.velocity = device.velocity;
            rigidbody.angularVelocity = device.angularVelocity;
        }
        //        rigidbody.velocity = device.velocity;
        //        rigidbody.angularVelocity = device.angularVelocity;
    }
}