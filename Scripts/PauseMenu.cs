using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause;
    public bool Paused = false;
    public AudioSource backgroundmusic;

    private void Start()
    {
        Pause.gameObject.SetActive(false);
        Cursor.visible = false;
        Paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Paused == true)
            {
                Paused = false;
                Cursor.visible = false;
                Pause.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                backgroundmusic.Play();
            }
            else
            {
                Pause.gameObject.SetActive(true);
                Cursor.visible = true;
                Paused = true;

                Time.timeScale = 0f;

                backgroundmusic.Pause();
            }
        }
    }

    public void Resume()
    {
        Pause.gameObject.SetActive(false);
        Cursor.visible = false;
        Paused = false;
        Time.timeScale = 1.0f;
        backgroundmusic.Play();
    }
}
