using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePush : MonoBehaviour
{
    [SerializeField] GameObject movableDestroyer;
    [SerializeField] GameObject bridge;
    [SerializeField] float pushSpeed = 2f;
    bool pushMovable = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            pushMovable = true;
        }
        else if (other.gameObject.CompareTag("MovableDestroyer"))
        {
            bridge.SetActive(true);
            Destroy(gameObject);
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
            Vector3 direction = movableDestroyer.transform.position - transform.parent.transform.position;
            direction = Vector3.Normalize(new Vector3(direction.x, 0, direction.y));
            transform.parent.position += pushSpeed * direction * Time.deltaTime;
        }
    }
}
