using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBox : MonoBehaviour
{

    public GameObject player;
    GameObserver observer;

    public Sprite spriteActive;
    public Sprite spriteInactive;

    BoxCollider2D boxCollider;
    SpriteRenderer renderer;
    bool active = false;
    bool collidesWithPlayer = false;
    
    void Start() {
        renderer = GetComponent<SpriteRenderer>();
        observer = GameObject.Find("GameObserver").GetComponent<GameObserver>();
        boxCollider = GetComponent<BoxCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (observer.flipped != active && tag == "Red") {
            FlipRed();
        } else if (observer.flipped != active && tag == "Blue") {
            FlipBlue();
        }
    }

    void FlipRed() {
        if (active) {
            boxCollider.isTrigger = true;
            renderer.sprite = spriteInactive;
        } else {
            Debug.Log(collidesWithPlayer);
            if (collidesWithPlayer) {
                GameObject.Find("Player").GetComponent<MovementController>().Explode();
                collidesWithPlayer = false;
            }
            boxCollider.isTrigger = false;
            renderer.sprite = spriteActive;
        }

        active = !active;
    }

    void FlipBlue() {
        if (!active) {
            boxCollider.isTrigger = true;
            renderer.sprite = spriteInactive;
        } else {
            Debug.Log(collidesWithPlayer);
            if (collidesWithPlayer) {
                GameObject.Find("Player").GetComponent<MovementController>().Explode();
                collidesWithPlayer = false;
            }
            boxCollider.isTrigger = false;
            renderer.sprite = spriteActive;
        }
        
        active = !active;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (active) {
            return;
        }
        if (other.gameObject.name.Equals("Player"))
        {
            collidesWithPlayer = true;
        }
    }
 
    void OnTriggerExit2D(Collider2D other) {
        if (active) {
            return;
        }
        if (other.gameObject.name.Equals("Player"))
        {
            collidesWithPlayer = false;
        }
    }
}
