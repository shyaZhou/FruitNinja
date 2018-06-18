using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            sphere.transform.position = transform.position;
            //sphere.transform.rotation = Quaternion.Euler(transform.localRotation.x - 90, transform.localRotation.y, transform.localRotation.z);
            sphere.transform.rotation=transform.rotation;
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
    void OnTriggerStay(Collider collider)
    {
        Debug.Log("Stay");
        if (collider.tag == "CanGrab")
        {
            Debug.Log("CangGrab");
            //物品抓取
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                collider.attachedRigidbody.isKinematic = true;
                sphere.transform.position = this.transform.Find("Model").position;
                sphere.transform.localRotation = Quaternion.Euler(
                  this.transform.Find("Model").localRotation.x+90,
                 this.transform.Find("Model").localRotation.y,
                this.transform.Find("Model").localRotation.z);
                
                collider.gameObject.transform.SetParent(gameObject.transform);
                if (collider.GetComponent<Axe>())
                {
                    collider.GetComponent<Axe>().axeState = Axe.AxeState.INHAND;
                }
                sphere.GetComponent<MeshCollider>().isTrigger = true;
            }
            //物品分离
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                collider.attachedRigidbody.isKinematic = false;
                collider.gameObject.transform.SetParent(null);
                tossObject(collider.attachedRigidbody);
                if (collider.GetComponent<Axe>())
                {
                    collider.GetComponent<Axe>().axeState = Axe.AxeState.INAIR;
                }
                sphere.GetComponent<MeshCollider>().isTrigger = false;
                //sphere.GetComponent<Rigidbody>().angularVelocity = device.angularVelocity;
                //sphere.GetComponent<Rigidbody>().velocity = device.velocity;
            }
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