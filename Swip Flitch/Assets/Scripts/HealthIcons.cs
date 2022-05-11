using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcons : MonoBehaviour
{
    [SerializeField] private Image imageRenderer;

    private float startTime = 0f;
    [SerializeField] private float holdTime = 5.0f;
    [SerializeField] private float blinkTime = 1.0f;
    private float timer = 0f;

    private bool switchImage = false;
    private bool timeToSwitch = true;

    // Start is called before the first frame update
    void Start()
    {
        imageRenderer.enabled = true;
        startTime = Time.time;
        timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (switchImage != timeToSwitch)
        {
            timer += Time.deltaTime;

            if (timer > (startTime + holdTime))
            {
                switchImage = !switchImage;
                timeToSwitch = !timeToSwitch;
                startTime = Time.time;
                timer = startTime;
            }
        }

        if(switchImage)
        {
            timer += Time.deltaTime;
            if (timer > (startTime + blinkTime))
            {
                switchImage = !switchImage;
                timeToSwitch = !timeToSwitch;
                startTime = Time.time;
                timer = startTime;
            }
        }

        if (!switchImage)
        {
            imageRenderer.enabled = true;
        }
        else
        {
            imageRenderer.enabled = false;
        }

    }
}
