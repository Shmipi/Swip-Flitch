using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour 
{
 
    public GameObject destination;
    public GameObject player; 
    BoxCollider2D boxCollider;
    bool collidesWithPlayer = false;
    [SerializeField] private GameObserver gameObserver;
    public MovementController movement; 


    void Teleporting() {
        player.transform.position = destination.transform.position;
        movement.ChangeRespawnPosition(destination);
    }


     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
        
            if (!gameObserver.flipped && tag == "Blue") {
               Teleporting();
               
            } else if (gameObserver.flipped && tag == "Red") {
               Teleporting();
              
            } 
        }
    }
}
