using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    [SerializeField] private GameObserver observer;

    public Transform target;
    public float smooth = 5.0f;
    public float offset = 1.0f;

    void LateUpdate()
    {
        if(observer.health > 0) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + offset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        } else
        {
            transform.position = transform.position;
        }
    }
}
