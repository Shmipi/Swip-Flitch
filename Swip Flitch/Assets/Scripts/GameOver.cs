using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObserver observer;
    [SerializeField] private GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (observer.health <= 0) {
            gameOverPanel.active = true;
        }
    }
}
