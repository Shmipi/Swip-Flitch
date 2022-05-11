using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition1;
    public Animator transition2;
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    public void Menu() {
        StartCoroutine(LoadMenu());
    }

    public IEnumerator LoadLevel()
    {
        transition1.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        transition2.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    public IEnumerator LoadMenu() {
        transition1.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        transition2.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
