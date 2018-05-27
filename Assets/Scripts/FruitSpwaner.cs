using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpwaner : MonoBehaviour {

    public GameObject[] fruitPrefab;
    private Object[] ob;
    private void Awake()
    {
        ob=Resources.LoadAll("FruitPrefab");
        fruitPrefab = new GameObject[ob.Length];
        for (int i = 0; i < ob.Length; i++)
        {
            fruitPrefab[i] = (GameObject)ob[i];
            if (fruitPrefab[i].GetComponent<Fruit>() == null)
            {
                fruitPrefab[i].AddComponent<Fruit>();
            }
        }
        if (fruitPrefab == null)
        {
            Debug.Log("FruitPrefab is Null!!!");
        }
    }
    // Use this for initialization
    void Start() {
        //StartCoroutine(SpwanFruit());        
    }
    public void StartSpwan()
    {
        StartCoroutine(ISpwanFruit());
    }
    private void FixedUpdate()
    {

    }
    IEnumerator ISpwanFruit()
    {
        while (true)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-1f, 1f);
            GameObject go=Instantiate(fruitPrefab[Random.Range(0, fruitPrefab.Length)], pos, Quaternion.Euler(-90f, 0f, 0f));
            Rigidbody temp;
            if (go.GetComponent<Rigidbody>() == null)
            {
                temp = go.AddComponent<Rigidbody>();
            }
            else
            {
                temp = go.GetComponent<Rigidbody>();
            }
            temp.useGravity = true;
            temp.angularDrag = 0f;
            float multiplier = 5f;
            temp.velocity = (transform.forward+transform.up)*multiplier;
            temp.angularVelocity=new Vector3(Random.Range(0f,12f),0f,0f);

            yield return new WaitForSeconds(1f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position,new Vector3(0.2f,0.2f,0.2f));
    }
}
