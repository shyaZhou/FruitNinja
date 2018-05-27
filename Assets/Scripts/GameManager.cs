using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GameType
    {
        Default=0,
        Start=1,
        Exit=2
    }
    public GameType gameType = GameType.Default;
    public GameObject hpOb;
    public List<GameObject> hpObsList;
    public GameObject spwaner;
    //hp
    public int maxHp=0;
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
            EndGame();
            if (gameType == GameType.Start)
            {
                gameType = GameType.Default;
                BeginManager.instance.CreateUI<UIStart>("UIStart", BeginManager.instance.mode1Prefab, "Start", Vector3.zero);
                BeginManager.instance.CreateUI<UIExit>("UIExit", BeginManager.instance.mode2Prefab, "Exit", new Vector3(1, 0, 0));
            }
        }
	}
    public void EndGame()
    {
        if (spwaner.activeSelf == true)
        {
            spwaner.SetActive(false);
        }
    }
    public void StartGame(int hpNum)
    {
        hp=maxHp = hpNum;
        spwaner.SetActive(true);
        spwaner.GetComponent<FruitSpwaner>().StartSpwan();
        GameObject hpBar = GameObject.Find("HpBar");   
        for (int i = 0; i < maxHp; i++)
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
