using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObserver : MonoBehaviour
{
    public bool flipped;

    public int health = 3;

    // Start is called before the first frame update
    void Start() {
        flipped = false;
        health = 3;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    public void FlipSwitch() {
        flipped = !flipped;
    }

    public void takeDamage()
    {
        health -= 1;
    }

}
