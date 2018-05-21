using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject spwaner;
    //hp
    public int hp = 3;
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
    public void StartGame()
    {
        hp = 3;
        spwaner.SetActive(true);
    }
    private void OnDestroy()
    {
        if (instance != null)
            instance = null;
    }
}
