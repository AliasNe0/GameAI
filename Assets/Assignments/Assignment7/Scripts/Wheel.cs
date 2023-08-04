using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSIGNMENT7
{
    public class Wheel : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 35f;

        Detection detection;
        float rotation = 0;

        void Awake()
        {
            detection = GetComponentInParent<Detection>();
        }

        void FixedUpdate()
        {
            if (!detection.CantFindWaypoint)
            {
                rotation += Time.deltaTime * rotationSpeed * Mathf.Rad2Deg;
                transform.localRotation = Quaternion.Euler(rotation, 0, 0);
            }
        }
    }
}
