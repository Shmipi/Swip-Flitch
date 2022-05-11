using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpike : MonoBehaviour
{
    public GameObject player;
    GameObserver observer;

    BoxCollider2D boxCollider;
    Animator animator;
    bool active = false;
    bool collidesWithPlayer = false;
    
    void Start() {
        animator = GetComponent<Animator>();
        observer = GameObject.Find("GameObserver").GetComponent<GameObserver>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (observer.flipped != active && name.Equals("RedSwitchSpike")) {
            RedFlip();
        } else if (observer.flipped != active && name.Equals("BlueSwitchSpike")) {
            BlueFlip();
        }
    }



    void RedFlip() {
        if (active) {
            animator.Play("SpikeAnimationDown");
            boxCollider.isTrigger = true;
        } else {
            Debug.Log(collidesWithPlayer);
            if (collidesWithPlayer) {
                GameObject.Find("Player").GetComponent<MovementController>().Explode();
                collidesWithPlayer = false;
            }
            boxCollider.isTrigger = false;
            animator.Play("SpikeAnimation");
        }


        active = !active;
    }

    void BlueFlip() {
        if (!active) {
            animator.Play("SpikeAnimationDown");
            boxCollider.isTrigger = true;
        } else {
            Debug.Log(collidesWithPlayer);
            if (collidesWithPlayer) {
                GameObject.Find("Player").GetComponent<MovementController>().Explode();
                collidesWithPlayer = false;
            }
            boxCollider.isTrigger = false;
            animator.Play("SpikeAnimation");
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
