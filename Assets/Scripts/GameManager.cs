using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // will be removed later
    [SerializeField]
    private GameObject towerPrefab;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}   
}