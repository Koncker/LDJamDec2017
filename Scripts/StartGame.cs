using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    public GameObject startScreen;
    public GameObject game;
    public GameObject engGame;

    private bool waitingToStartGame = true;
    public bool AllowStart = true;


	void Start ()
    {
		if (startScreen != null)
        {
            startScreen.SetActive(true);
        }
        else
        {
            waitingToStartGame = false;
            Debug.LogError("startScreen was not set in the inspector.");
        }

        if (game != null)
        {
            game.SetActive(false);
        }
        else
        {
            Debug.LogError("game was not set in the inspector.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (AllowStart && waitingToStartGame && (Input.GetButtonDown("Submit")))
        {
            waitingToStartGame = false;
            if (startScreen != null)
            {
                startScreen.SetActive(false);
            }
            if (game != null)
            {
                game.SetActive(true);
                Time.timeScale = 1;
            }
        }
	}
}
