using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ASSIGNMENT7
{
    public class Steering : MonoBehaviour
    {
        [SerializeField] float speed = 25f;
        [SerializeField] float rotationSpeed = 3.5f;

        Detection detection;

        void Awake()
        {
            detection = GetComponent<Detection>();
        }

        void FixedUpdate()
        {
            if (detection.CantFindWaypoint) return;
            GameObject waypoint = detection.NextWaypoint;
            if (!waypoint) return;
            Vector3 direction = transform.forward + rotationSpeed * Time.deltaTime * Vector3.Normalize(waypoint.transform.position - transform.position);
            direction = new Vector3(direction.x, 0, direction.z);
            transform.position += speed * Time.deltaTime * direction;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
