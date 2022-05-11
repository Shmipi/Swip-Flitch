using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    [SerializeField] int levelToLoad;
    private LevelLoader lL;

    void Start() {
    lL = FindObjectOfType<LevelLoader>();
    }
    

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
        lL.Menu();
        }
    }
}
