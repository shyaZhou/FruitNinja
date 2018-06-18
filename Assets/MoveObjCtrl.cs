using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjCtrl : MonoBehaviour {
    public List<GameObject> MoveObj = new List<GameObject>();
    public float speed=1f;
    public Transform StartPos;
    public Transform EndPos;
	// Use this for initialization
	void Start () {
        foreach (Transform t in this.transform)
        {
            if (t.tag == "MoveObj")
            {
                MoveObj.Add(t.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var item in MoveObj)
        {
            float x = EndPos.transform.position.x, y = EndPos.transform.position.y, z = EndPos.transform.position.z;
            item.transform.Translate(Vector3.right * speed*Time.deltaTime);
            if (item.transform.position.x > EndPos.position.x)
            {
                item.transform.position = new Vector3(Random.Range(x - 20, 2 + 20), y, Random.Range(z - 20, z + 20));
            }
        }
	}
}
