using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Image deadHealthBoi1;
    [SerializeField] private Image deadHealthBoi2;
    [SerializeField] private Image deadHealthBoi3;

    [SerializeField] private GameObserver observer;

    // Start is called before the first frame update
    void Start()
    {
        deadHealthBoi1.enabled = false;
        deadHealthBoi2.enabled = false;
        deadHealthBoi3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (observer.health >= 3)
        {
            deadHealthBoi1.enabled = false;
            deadHealthBoi2.enabled = false;
            deadHealthBoi3.enabled = false;
        } else if (observer.health == 2)
        {
            deadHealthBoi1.enabled = false;
            deadHealthBoi2.enabled = false;
            deadHealthBoi3.enabled = true;
        } else if (observer.health == 1)
        {
            deadHealthBoi1.enabled = false;
            deadHealthBoi2.enabled = true;
            deadHealthBoi3.enabled = true;
        } else
        {
            deadHealthBoi1.enabled = true;
            deadHealthBoi2.enabled = true;
            deadHealthBoi3.enabled = true;
        }

    }
}
