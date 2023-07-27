using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePush : MonoBehaviour
{
    [SerializeField] GameObject movableSocket;
    [SerializeField] float pushSpeed = 5f;
    bool pushMovable = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            pushMovable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            pushMovable = false;
        }
    }

    void FixedUpdate()
    {
        if (pushMovable)
        {
            Vector3 direction = movableSocket.transform.position - transform.parent.transform.position;
            direction = Vector3.Normalize(new Vector3(direction.x, 0, direction.y));
            transform.parent.position += pushSpeed * direction * Time.deltaTime;
        }
    }
}
