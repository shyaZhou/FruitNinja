using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GameType
    {
        Default = 0,
        Start = 1,
        Exit = 2,
        GameOver = 3
    }
    public FruitSpwaner fruitSpwaner;
    public GameObject Timer;
    public GameType gameType = GameType.Default;
    public GameObject hpOb;
    public List<GameObject> hpObsList;
    public GameObject spwaner;
    //hp
    public int maxHp = 0;
    public int hp = 0;
    public static GameManager instance;
    public float time = 0f;
    public GameObject Life;
    float timeTemp;
    public GameManager()
    {
        if (instance == null)
            instance = this;
    }
    public IEnumerator ITimer = null;

    void Awake()
    {
        spwaner = GameObject.Find("GamePlay").transform.Find("Spawner").gameObject;
        time = 60f;
    }
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Life.GetComponent<Text>().text = "" + hp;
        if (gameType == GameType.Default)
        {
            Timer.GetComponent<Text>().text = "";
        }
        else if ((gameType == GameType.GameOver))
        {
            Timer.GetComponent<Text>().text = "GameOver";
            if (Time.time - timeTemp > 1f && timeTemp > 0)
            {
                BeginManager.instance.CreateUI<UIStart>("UIStart", BeginManager.instance.mode1Prefab, "Start", Vector3.zero);
                BeginManager.instance.CreateUI<UIExit>("UIExit", BeginManager.instance.mode2Prefab, "Exit", new Vector3(1, 0, 0));
                timeTemp = -1f;
            }
        }
        else
        {
            Timer.GetComponent<Text>().text = "" + (int)(time);
        }
        if (hp <= 0 || time < 0f)
        {
            EndGame();
            StopTime();
            time = 0f;
            if (gameType == GameType.Start)
            {
                gameType = GameType.GameOver;
                timeTemp = Time.time;
            }
        }
    }
    public void EndGame()
    {
        if (fruitSpwaner.ISpwan != null)
        {
            StopCoroutine(fruitSpwaner.ISpwan);
        }
        fruitSpwaner.ISpwan = null;
        if (spwaner.activeSelf == true)
        {
            spwaner.SetActive(false);
        }
    }
    public void StartGame(int hpNum)
    {
        hp = maxHp = hpNum;
        spwaner.SetActive(true);
        spwaner.GetComponent<FruitSpwaner>().StartSpwan();
        GameObject hpBar = GameObject.Find("HpBar");
        for (int i = 0; i < maxHp; i++)
        {
            hpObsList.Add(Instantiate(hpOb));
            hpObsList[i].transform.parent = hpBar.transform;
            hpObsList[i].transform.localPosition = new Vector3((float)i * -1.5f, 0f, 0f);
        }

    }
    private void OnDestroy()
    {
        if (instance != null)
            instance = null;
    }
    public void StartTime(float t)
    {
        ITimer = SetTime(t);
        StartCoroutine(ITimer);
    }
    public void StopTime()
    {
        if (ITimer != null)
        {
            StopCoroutine(ITimer);
            ITimer = null;
        }
    }
    IEnumerator SetTime(float t)
    {
        float timeT = Time.time + t;
        for (int i = 0; i <= t; i++)
        {
            time = timeT - Time.time;
            yield return new WaitForSeconds(1);
        }
    }

}
