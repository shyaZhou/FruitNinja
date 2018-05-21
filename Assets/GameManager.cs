using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject hpOb;
    public List<GameObject> hpObsList;
    public GameObject spwaner;
    //hp
    public int hp=0;
    public static GameManager instance;
    public GameManager()
    {
        if (instance == null)
            instance = this;
    }

    void Awake()
    {
        spwaner = GameObject.Find("GamePlay").transform.Find("Spawner").gameObject;
    }
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (hp == 0)
        {
            spwaner.SetActive(false);
        }
	}
    public void StartGame(int hpNum)
    {
        hp = hpNum;
        spwaner.SetActive(true);
        GameObject hpBar = GameObject.Find("HpBar");   
        for (int i = 0; i < hp; i++)
        {
            hpObsList.Add(Instantiate(hpOb));
            hpObsList[i].transform.parent = hpBar.transform;
            hpObsList[i].transform.localPosition = new Vector3((float)i*-1.5f, 0f, 0f);
        }

    }
    private void OnDestroy()
    {
        if (instance != null)
            instance = null;
    }
}
