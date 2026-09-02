using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diemenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsOver = false;
    public GameObject diemenu;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
            if (!GameIsOver)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        
    }
    public void Pause()
    {
        diemenu.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsOver = true;
    }
    public void Resume()
    {
        diemenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsOver = false;
    }

    public void Mainmenu()
    {
        Time.timeScale = 1.0f;
        GameIsOver = false;
        SceneManager.LoadScene("Start");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
