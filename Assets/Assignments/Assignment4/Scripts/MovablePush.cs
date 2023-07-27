using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePush : MonoBehaviour
{
    [SerializeField] float pushSpeed = 5f;
    bool pushMovable = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Agent"))
        {
            pushMovable = true;
        }
    }

    private void OnTriggerExit(Collider other)
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
            Vector3 direction = Vector3.Normalize(transform.parent.transform.position - transform.position);
            transform.parent.position += pushSpeed * direction * Time.deltaTime;
        }
    }
}
