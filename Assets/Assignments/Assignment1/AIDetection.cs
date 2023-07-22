using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASSIGNMENT1;

namespace ASSIGNMENT1
{
    public class AIDetection : MonoBehaviour
    {
        [SerializeField]
        float distanceRange = 10f;
        [SerializeField]
        float rays = 10f;
        [SerializeField]
        float angleRange = 180f;

        void FixedUpdate()
        {
            for (int i = 0; i <= rays; i++)
            {
                float rayFactor = 2f * i / rays - 1f;
                float rayAngle = angleRange * Mathf.Deg2Rad / 2 * rayFactor;
                Vector3 rayRotation = new Vector3(Mathf.Sin(rayAngle), 0, Mathf.Cos(rayAngle));
                Vector3 rayEndPosition = transform.position + rayRotation * distanceRange;
                Debug.DrawLine(transform.position, rayEndPosition, Color.red);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayEndPosition, out hit))
                {
                    Debug.DrawLine(transform.position, rayEndPosition, Color.green);
                }

            }
        }
    }
}
