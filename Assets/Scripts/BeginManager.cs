using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginManager : MonoBehaviour {
    public static BeginManager instance;
    private GameObject[] _fruitPrefab;
    private object[] _ob;
    private int _FruitNum;
    public GameObject mode1Prefab;
    public GameObject mode2Prefab;
    public GameObject board;
    public List<GameObject> rotateObjectList=new List<GameObject>();
    public List<UIBase> uiBaseList = new List<UIBase>();
    public float rotateSpeed=20f;
    public Sprite startSprite;
    public Sprite exitSprite;
    private void Awake()
    {
        if (instance==null)
            instance = this;
        rotateObjectList = new List<GameObject>();
        _ob = Resources.LoadAll("FruitPrefab");
        _fruitPrefab = new GameObject[_ob.Length];
        _FruitNum = _ob.Length;
        for (int i = 0; i < _ob.Length; i++)
        {
            _fruitPrefab[i] = (GameObject)_ob[i];
            if (_fruitPrefab[i].GetComponent<Fruit>() == null)
            {
                _fruitPrefab[i].AddComponent<Fruit>();
            }
        }
        if (_fruitPrefab == null)
        {
            Debug.LogError("FruitPrefab is Null!!!");
        }
        mode1Prefab = _fruitPrefab[0];
        mode2Prefab = _fruitPrefab[1];
        CreateUI<UIStart>("UIStart", mode1Prefab, "Start", Vector3.zero);
        CreateUI<UIExit>("UIExit", mode2Prefab, "Exit", new Vector3(1, 0, 0));
    }
    private void OnDestroy()
    {
        if (instance != null)
            instance = null;
    }
    // Use this for initialization
    void Start () {
        //GameObject ob = Instantiate(mode1Prefab);
        //ob.transform.position = this.transform.position;
        for (int i = 0; i < _fruitPrefab.Length; i++)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
        //List空则返回
        if (rotateObjectList.Count == 0)
        {
            return;
        }
        else {
            for (int i = 0; i < rotateObjectList.Count; i++)
            {
                //有物体但是未激活
                if (rotateObjectList[i].activeSelf == false)
                {
                    return;
                }
                else
                rotateObjectList[i].transform.Rotate(transform.forward, rotateSpeed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ShowDeBug();
        }
    }
    public void CreateUI<T>(string name,GameObject UIObject,string typeName,Vector3 pos) where T:UIBase
    {
        GameObject beginManager=GameObject.Find("BeginManager");
        //将本层挂在BeginManager下
        GameObject parentOb = new GameObject(name);
        parentOb.transform.parent = beginManager.transform;
        parentOb.transform.localPosition = pos;
        //外边框
        GameObject _border = Instantiate(board) as GameObject;
        _border.transform.parent = parentOb.transform;
        _border.transform.localPosition = Vector3.zero;
        //文字
        GameObject text = new GameObject("text");
        text.AddComponent<SpriteRenderer>();
        text.transform.parent = parentOb.transform;
        text.transform.localPosition = Vector3.zero;
        text.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        text.transform.Rotate(transform.up, 180f);
        //旋转的物体
        rotateObjectList.Add(Instantiate(UIObject));
        rotateObjectList[rotateObjectList.Count - 1].tag = "UI";
        rotateObjectList[rotateObjectList.Count - 1].transform.parent = parentOb.transform;
        rotateObjectList[rotateObjectList.Count - 1].transform.localPosition = Vector3.zero;
        //设置<T>并完成绑定
        T uiTemp = rotateObjectList[rotateObjectList.Count - 1].AddComponent<T>();
        uiBaseList.Add(uiTemp);
        uiTemp.index = rotateObjectList.Count - 1;
        uiTemp.uiType=(UIBase.UIType)Enum.Parse(typeof(UIBase.UIType),typeName);
        uiTemp.owner = rotateObjectList[rotateObjectList.Count - 1];
    }
    public void DestroyUIAll()
    {
        for (int i = 0; i < rotateObjectList.Count; i++)
        {
            Destroy(rotateObjectList[i].transform.parent.gameObject,1f);
        }
        rotateObjectList.Clear();
        uiBaseList.Clear();
    }
    public void HideUIAll()
    {
        for (int i = 0; i < rotateObjectList.Count; i++)
        {
            rotateObjectList[i].transform.parent.gameObject.SetActive(false);
        }
    }
    public void ShowUIAll()
    {
        for (int i = 0; i < rotateObjectList.Count; i++)
        {
            rotateObjectList[i].transform.parent.gameObject.SetActive(true);
        }
    }
    void ShowDeBug()
    {
        Debug.Log("count:" + rotateObjectList.Count);
        BeginManager.instance.rotateObjectList.RemoveAt(0);
    }
}
