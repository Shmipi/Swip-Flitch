using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{

    [SerializeField] private GameObject red;
    [SerializeField] private GameObject blue;

    [SerializeField] private GameObserver gameObserver;

    //private bool isBlue = true;
    
    void Start()
    {
        red.SetActive(false);
        blue.SetActive(true);
        //isBlue = true;
    }

    void Update()
    {
        if(gameObserver.flipped)
        {
            red.SetActive(true);
            blue.SetActive(false);
            //isBlue = false;
        } else
        {
            red.SetActive(false);
            blue.SetActive(true);
        }
    }
}
