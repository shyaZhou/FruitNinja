using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Rightss : MonoBehaviour
{
    //追踪的设备，就是我们的手柄
    SteamVR_TrackedObject trackedObj;
    //获取手柄的输入
    SteamVR_Controller.Device device;
    //固定关节
    FixedJoint fixedJoint;
    //位于手柄上的刚体
    public Rigidbody rigidBodyAttachPoint;
    public Transform sphere;
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    void FixedUpdate()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        //重置功能
        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //位置
            sphere.transform.position = transform.position;
            //速度
            sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //角速度
            sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
    void OnTriggerStay(Collider collider)
    {
        if (fixedJoint == null && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //把关节组件添加到实例化对象上，连接的为位置就是这个刚体
            fixedJoint = collider.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidBodyAttachPoint;
        }
        else if (fixedJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            //获取关节上的游戏对象
            GameObject go = fixedJoint.gameObject;
            //获取刚体
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            //销毁
            Object.Destroy(fixedJoint);
            //设置为空
            fixedJoint = null;
            tossObject(rigidbody);
        }
    }
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
    }
}