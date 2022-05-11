using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    LevelLoader lL;

    public void Start()
    {
        lL = FindObjectOfType<LevelLoader>();
    }

    public void StartGame() {
        lL.LoadNextLevel();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void MainMenu() {
        lL.Menu();
    }

   
}
