using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }

    public GameObject losePanel;

    // Use this for initialization
    void Start ()
    {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void perder()
    {
        losePanel.SetActive(true);
    }

    public void Reinciar()
    {
        SceneManager.LoadScene(0);
    }
}
